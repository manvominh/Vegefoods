import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import { HomeComponent } from './Pages/home/home.component';
import { ProductsComponent } from './Pages/products/products.component';
import { ProductDetailsComponent } from './Pages/product-details/product-details.component';
import { RegisterComponent } from './Pages/users/register/register.component';
import { LoginComponent } from './Pages/users/login/login.component';
import { ProfileComponent } from './Pages/users/profile/profile.component';
import { FooterComponent } from './Pages/partials/footer/footer.component';
import { NavbarComponent } from './Pages/partials/navbar/navbar.component';
import { AboutComponent } from './Pages/about/about.component';
import { UserService } from './Services/user.service';
import { LoadingComponent } from './Pages/partials/loading/loading.component';
import { ProductService } from './Services/product.service';
import { AuthService } from './Services/auth.service';
import { AuthInterceptorService } from './Services/auth-interceptor.service';
import { PageNotFoundComponent } from './Pages/page-not-found/page-not-found.component';
import { GlobalErrorComponent } from './Pages/global-error/global-error.component';
import { GlobalErrorHandlerService } from './Services/global-error-handler.service';

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
    NavbarComponent,
    AboutComponent,
    LoadingComponent,
    PageNotFoundComponent,
    GlobalErrorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [
    UserService,
    ProductService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    GlobalErrorHandlerService,
    { provide: ErrorHandler, useClass: GlobalErrorHandlerService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
