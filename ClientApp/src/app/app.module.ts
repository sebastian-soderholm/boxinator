import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// Firebase
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFirestoreModule } from '@angular/fire/compat/firestore';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';
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


const firebaseConfig = {
  apiKey: "AIzaSyDmiXP0l04AtH2WqqP9m0DAq8Pxo-_rJPA",
  authDomain: "boxinator.firebaseapp.com",
  projectId: "boxinator",
  storageBucket: "boxinator.appspot.com",
  messagingSenderId: "789898616661",
  appId: "1:789898616661:web:1b7959121446003ec9d9dc",
  measurementId: "G-LCX1WECRNE"
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
    CountrySettingsPage,
    GuestShipmentPage,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFireModule.initializeApp(firebaseConfig),
    AngularFirestoreModule, // firestore
    AngularFireAuthModule, // auth
    AngularFireStorageModule // storage
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
