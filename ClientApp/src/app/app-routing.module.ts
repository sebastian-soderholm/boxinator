import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPage } from './login/pages/login/login.page';
import { RegisterPage } from './login/pages/register/register.page';
import { DashboardPage } from './shipment/pages/dashboard/dashboard.page';
import { AuthGuard } from './login/services/auth.guard';
import { MyShipmentsPage } from './shipment/pages/my-shipments/my-shipments.page';
import { NewShipmentPage } from './shipment/pages/new-shipment/new-shipment.page';
import { MyAccountPage } from './account/pages/my-account/my-account.page';
import { EditAccountPage } from './account/pages/edit-account/edit-account.page';
import { GuestShipmentPage } from './shipment/pages/guest-shipment/guest-shipment.page';
import { SettingsPage } from './admin/pages/settings/settings.page';
import { EditShipmentPage } from './shipment/pages/edit-shipment/edit-shipment.page';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/login'
  },
  {
    path: 'login',
    component: LoginPage
  },
  {
    path: 'register',
    component: RegisterPage,
  },
  {
    path: 'guest',
    component: GuestShipmentPage,
  },
  {
    path: 'dashboard',
    component: DashboardPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'my-shipments',
    component: MyShipmentsPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'new-shipment',
    component: NewShipmentPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'my-account',
    component: MyAccountPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'my-account/edit',
    component: EditAccountPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'settings',
    component: SettingsPage,
    canActivate: [AuthGuard]
  },
  {
    path: 'edit-shipment/:id',
    component: EditShipmentPage,
    canActivate: [AuthGuard]
  },
  {path: '**', redirectTo: ''},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
