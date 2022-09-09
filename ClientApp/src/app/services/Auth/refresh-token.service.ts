import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Toast, ToastrService } from 'ngx-toastr';
import { lastValueFrom, Observable } from 'rxjs';
import { RefreshToken } from 'src/app/models/refreshToken.model';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root',
})
export class RefreshTokenService implements CanActivate {
  public jwtHelper: JwtHelperService = new JwtHelperService();

  refreshTokenModel: RefreshToken = {
    token: '',
    refreshToken: '',
  };

  constructor(
    private router: Router,
    private http: HttpClient,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  async canActivate() {
    const token = localStorage.getItem('token');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }

    const refreshSuccess = await this.refreshingTokenAsync(token);

    if (!refreshSuccess) {
      this.router.navigate(['/login']);
    }
    return refreshSuccess;
  }

  private async refreshingTokenAsync(token: string | null): Promise<boolean> {
    const refreshToken: string | null = localStorage.getItem('refreshToken');

    if (!token || !refreshToken) {
      return false;
    }
    this.refreshTokenModel.token = token;
    this.refreshTokenModel.refreshToken = refreshToken;
    // const refreshTokenModel = JSON.stringify(this.refreshTokenMode);

    let refreshSuccess: boolean;

    try {
      const response = await lastValueFrom(
        this.authService.refreshToken(this.refreshTokenModel)
      );
      const newToken = (<any>response).token;
      const newRefreshToken = (<any>response).refreshToken;

      console.log(response);
      localStorage.setItem('token', newToken);
      localStorage.setItem('refreshToken', newRefreshToken);
      this.toastr.success('Refresh token Successfully');
      refreshSuccess = true;
    } catch (ex) {
      this.toastr.error('Error occures');
      refreshSuccess = false;
    }

    return refreshSuccess;
  }
}
