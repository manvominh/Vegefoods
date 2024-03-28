import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { ProductsComponent } from './Pages/products/products.component';
import { LoginComponent } from './Pages/users/login/login.component';
import { RegisterComponent } from './Pages/users/register/register.component';
import { ProfileComponent } from './Pages/users/profile/profile.component';
import { AboutComponent } from './Pages/about/about.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full', title: 'Vegefoods Angular UI'  }, // Default redirection to 'home'
  { path: 'shop', component: ProductsComponent  },
  { path: 'login', component: LoginComponent  },
  { path: 'register', component: RegisterComponent  },
  { path: 'profile', component: ProfileComponent  },
  { path: 'about', component: AboutComponent  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
