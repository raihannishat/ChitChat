import { Injectable, Injector } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import {
  BehaviorSubject,
  catchError,
  filter,
  Observable,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import { AuthService } from '../auth.service';
import { RefreshToken } from 'src/app/models/refreshToken.model';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  refreshTokenModel: RefreshToken = {
    token: '',
    refreshToken: '',
  };
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );
  constructor(private authService: AuthService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<Object>> {
    let authReq = req;
    const token = this.authService.getAccessToken();

    if (!!token) {
      authReq = this.addTokenHeader(req, token);
    }

    return next.handle(authReq).pipe(
      catchError((error) => {
        if (
          error instanceof HttpErrorResponse &&
          !authReq.url.includes('auth/sigin') &&
          error.status === 401
        ) {
          return this.handle401Error(authReq, next);
        }
        return throwError(error);
      })
    );
  }
  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);
      this.refreshTokenModel = {
        token: this.authService.getAccessToken(),
        refreshToken: this.authService.getRefreshToken(),
      };

      if (this.refreshTokenModel.refreshToken) {
        return this.updateAccessToken(next, request);
      }
    }
    return this.refreshTokenSubject.pipe(
      filter((token) => token !== null),
      take(1),
      switchMap((token) => next.handle(this.addTokenHeader(request, token)))
    );
  }
  private updateAccessToken(next: HttpHandler, request: HttpRequest<any>) {
    return this.authService.refreshToken(this.refreshTokenModel).pipe(
      switchMap((result: any) => {
        this.isRefreshing = false;
        this.authService.saveAccessToken(result.token);
        this.authService.saveRefreshToken(result.refreshToken);
        this.refreshTokenSubject.next(result.token);
        return next.handle(this.addTokenHeader(request, result.token));
      }),
      catchError((err) => {
        this.isRefreshing = false;
        this.authService.logOut();
        return throwError(err);
      })
    );
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({
      headers: request.headers.set('Authorization', 'Bearer ' + token),
    });
  }
}
