import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/Auth/authService.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  userData: any;
  private userSub: Subscription=new Subscription;

  constructor(
    private authServ:AuthService
  ) {
  }
  isAuthenticated = false;

  ngOnInit(): void {
    const userDataString = localStorage.getItem('userData');
    this.userData = userDataString ? JSON.parse(userDataString) : null;
    console.log(this.userData);

    this.userSub = this.authServ.user.subscribe((user) => {
      this.isAuthenticated = !!user;
    });

  }

  logout(){
    this.authServ.logout();
    this.isAuthenticated=false;
    console.log(this.userData)
  }
}
