import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatComponent } from './component/chat/chat.component';
import { ChatboxComponent } from './component/chatbox/chatbox.component';
import { ChatpageComponent } from './component/chatpage/chatpage.component';
import { LoginComponent } from './component/login/login.component';
import { RegistrationComponent } from './component/registration/registration.component';
import { UserListComponent } from './component/user-list/user-list.component';
import { UserProfileComponent } from './component/user-profile/user-profile.component';
import { AuthGuard } from './services/Auth/auth-guard.service';

const routes: Routes = [
  {
    path: 'register',
    component: RegistrationComponent,
    canActivate: [AuthGuard],
  },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  {
    path: 'profile',
    component: UserProfileComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'users',
    component: UserListComponent,
    canActivate: [AuthGuard],
  },
  { path: 'chat', component: ChatComponent, canActivate: [AuthGuard] },
  //{path:"messages",component:MemberMessageComponent},

  {
    path: 'chatbox',
    component: ChatboxComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'chatpage',
    component: ChatpageComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
