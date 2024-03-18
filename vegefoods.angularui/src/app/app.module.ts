import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Pages/home/home.component';
import { ProductsComponent } from './Pages/products/products.component';
import { ProductDetailsComponent } from './Pages/product-details/product-details.component';
import { RegisterComponent } from './Pages/users/register/register.component';
import { LoginComponent } from './Pages/users/login/login.component';
import { ProfileComponent } from './Pages/users/profile/profile.component';
import { FooterComponent } from './Pages/partials/footer/footer.component';
import { NavbarComponent } from './Pages/partials/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductsComponent,
    ProductDetailsComponent,
    RegisterComponent,
    LoginComponent,
    ProfileComponent,
    FooterComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
