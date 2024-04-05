import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../Pages/users/user';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl:string = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  registerUser = (registeredData: User) => this.httpClient.post<User>(`${this.baseUrl}/Users/register`, registeredData);
  login = (loginData: User) => {
    return this.httpClient.post<User>(`${this.baseUrl}/Users/login`, loginData);
  }
  getUserByEmail = (email: string) => this.httpClient.get<User>(`${this.baseUrl}/Users/GetUserByEmail/${email}`);
  updateUser = (updateData: User, userId: number) => this.httpClient.put<User>(`${this.baseUrl}/Users/${userId}`, updateData);
}
