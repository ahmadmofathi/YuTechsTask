import { Injectable } from '@angular/core';
import { AuthService } from '../_Services/Auth/authService.service';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, take, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> {
    return this.authService.user.pipe(
      // Take latest value from subscription, then unsubscribe
      take(1),
      map((user) => {
        const isAuth = !!user.token; // Check if the user has a token
        if (isAuth) {
          return true;
        } else {
          // If not authenticated, redirect to login page
          return this.router.createUrlTree(['/login']);
        }
      })
    );
  }
}
