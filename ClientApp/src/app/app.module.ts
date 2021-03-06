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
import { AngularFireAuthModule, PERSISTENCE } from '@angular/fire/compat/auth';

// Angular Material
import { MatTableModule } from '@angular/material/table';
import {MatSidenavModule} from '@angular/material/sidenav';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, MatNativeDateModule, MAT_DATE_FORMATS } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import {MatIconModule} from '@angular/material/icon';
import {MatChipsModule} from '@angular/material/chips';

//Date picker format
import { MAT_DATE_LOCALE } from '@angular/material/core'

//Pages
import { LoginPage } from './login/pages/login/login.page';
import { RegisterPage } from './login/pages/register/register.page';
import { DashboardPage } from './shipment/pages/dashboard/dashboard.page';
import { MyShipmentsPage } from './shipment/pages/my-shipments/my-shipments.page';
import { NewShipmentPage } from './shipment/pages/new-shipment/new-shipment.page';
import { MyAccountPage } from './account/pages/my-account/my-account.page';
import { EditAccountPage } from './account/pages/edit-account/edit-account.page';
import { MenuPage } from './navigation/pages/menu/menu.page';
import { GuestShipmentPage } from './shipment/pages/guest-shipment/guest-shipment.page';
import { environment } from '../environments/environment';
import { DashboardListComponent } from './shipment/components/dashboard-list/dashboard-list.component';
import { DashboardListItemComponent } from './shipment/components/dashboard-list-item/dashboard-list-item.component';

//Color picker
import { ColorPickerModule } from 'ngx-color-picker';
import { BoxFormComponent } from './shipment/components/box-form/box-form.component';
import { SettingsPage } from './admin/pages/settings/settings.page';
import { CountrySettingsComponent } from './admin/components/country-settings/country-settings.component';
import { UserSettingsComponent } from './admin/components/user-settings/user-settings.component';
import { DatePipe } from '@angular/common';
import { CountryListComponent } from './admin/components/country-list/country-list.component';
import { CountryListItemComponent } from './admin/components/country-list-item/country-list-item.component';
import { EditShipmentPage } from './shipment/pages/edit-shipment/edit-shipment.page';
import { SharedFormComponent } from './shipment/components/shared-form/shared-form.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS, MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { UserSettingsFormComponent } from './admin/components/user-settings-form/user-settings-form.component';

const DATE_FORMAT = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MM/YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MM/YYYY',
  },
};


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
    GuestShipmentPage,
    DashboardListComponent,
    DashboardListItemComponent,
    BoxFormComponent,
    SettingsPage,
    CountrySettingsComponent,
    UserSettingsComponent,
    EditShipmentPage,
    CountryListComponent,
    CountryListItemComponent,
    SharedFormComponent,
    UserSettingsFormComponent
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
    ColorPickerModule,
    MatExpansionModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatMomentDateModule,
    MatListModule,
    MatIconModule,
    MatChipsModule
  ],
  providers: [
    DatePipe,
    MatSnackBar,
    { provide: PERSISTENCE, useValue: 'local' },
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    {provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS},

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
