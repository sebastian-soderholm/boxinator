import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';

// Firebase
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFirestoreModule } from '@angular/fire/compat/firestore';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';

// Angular Material
import { MatTableModule } from '@angular/material/table';
import {MatSidenavModule} from '@angular/material/sidenav';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';

//Pages
import { LoginPage } from './login/pages/login/login.page';
import { RegisterPage } from './login/pages/register/register.page';
import { DashboardPage } from './shipment/pages/dashboard/dashboard.page';
import { MyShipmentsPage } from './shipment/pages/my-shipments/my-shipments.page';
import { NewShipmentPage } from './shipment/pages/new-shipment/new-shipment.page';
import { MyAccountPage } from './account/pages/my-account/my-account.page';
import { EditAccountPage } from './account/pages/edit-account/edit-account.page';
import { MenuPage } from './navigation/pages/menu/menu.page';
import { CountrySettingsPage } from './admin/pages/country-settings/country-settings.page';
import { GuestShipmentPage } from './shipment/pages/guest-shipment/guest-shipment.page';
import { environment } from '../environments/environment';
import { DashboardListComponent } from './shipment/components/dashboard-list/dashboard-list.component';
import { DashboardListItemComponent } from './shipment/components/dashboard-list-item/dashboard-list-item.component';

//Material form modules
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';

//Color picker
import { ColorPickerModule } from 'ngx-color-picker';
import { BoxFormComponent } from './shipment/components/box-form/box-form.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginPage,
    RegisterPage,
    DashboardPage,
    MyShipmentsPage,
    NewShipmentPage,
    MyAccountPage,
    EditAccountPage,
    MenuPage,
    CountrySettingsPage,
    GuestShipmentPage,
    DashboardListComponent,
    DashboardListItemComponent,
    BoxFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFirestoreModule, // firestore
    AngularFireAuthModule, // auth
    AngularFireStorageModule, // storage
    MatTableModule,
    MatButtonToggleModule,
    MatSortModule,
    MatMenuModule,
    MatButtonModule,
    MatCardModule,
    MatProgressBarModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
    ColorPickerModule
  ],
  providers: [
    MatSelectModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
