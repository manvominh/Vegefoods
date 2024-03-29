import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  getToken(): string {
    
    let token = localStorage.getItem('token_vegefoods_angular');
    if(token) return token;
    return '';
  }
}
