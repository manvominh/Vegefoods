import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../Pages/users/user';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private userService: UserService) { 
    const token = localStorage.getItem('token_vegefoods_angular');
    //console.log(token);
    this._isLoggedIn$.next(!!token);
  }

  login = (loginData: User) => {
    return this.userService.login(loginData).pipe(
      tap((response: any) => {
        this._isLoggedIn$.next(true);
        
        localStorage.setItem('token_vegefoods_angular', response.token);
      })
    );
  }

  logout(){
    localStorage.removeItem('token_vegefoods_angular');
    localStorage.removeItem('email_vegefoods_angular');
    this._isLoggedIn$.next(false);
  }
  getToken(): string {
    
    let token = localStorage.getItem('token_vegefoods_angular');
    if(token) return token;
    return '';
  }
}
