import { inject } from '@angular/core';
import {  ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateFn,} from '@angular/router';
import { AuthService } from '../Services/auth.service';

import { map, take } from 'rxjs';

export const canActivateGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {

    let isLoggedinUser;
    inject(AuthService).isLoggedIn$.pipe(
        take(1),
        map(authState => {
            //console.log(authState);
            isLoggedinUser = authState;
        })
    ).subscribe(); 
   
    if (isLoggedinUser) {
        return true;
    } else {
        inject(Router).navigate(['/login']); // Use inject(Router) to get the Router service
        return false;
    }  
};