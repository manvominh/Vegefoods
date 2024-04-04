import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(public authService: AuthService,
    private router: Router
  ) {}

  public logout() {
    //console.log('logoff');
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
