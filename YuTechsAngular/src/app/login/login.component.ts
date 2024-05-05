import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/Auth/authService.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required]),
  });
  noErr:boolean=false;
  error:string='';
  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}
  ngOnInit(): void {
  }


  onSubmit() {
    if (this.loginForm.valid) {

      console.log(this.loginForm);
      this.authService
        .login(this.loginForm.value['username'], this.loginForm.value['password'])
        .subscribe(
        (loginData) => {
            // const userdata = JSON.parse(localStorage.getItem('userData')??'{}');
            // console.log(userdata.user_id);
            this.router.navigate(['authors']);
        },
        (error)=>{
          this.noErr=true;
          this.error = error.errorMessage;
          console.log(error);
        });
    }
  }
}
