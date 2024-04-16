import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { CartService } from '../../../Services/cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  count:number = 0;
  constructor(public authService: AuthService,
    private cartService:CartService,
    private router: Router
  ) {}

  ngOnInit() {    
  }

  getNumberItemsOfCart(): number{
    return this.cartService.getNumberItemsOfCart();
  };

  public logout() {
    //console.log('logoff');
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
