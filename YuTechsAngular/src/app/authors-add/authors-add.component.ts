import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorAddDTO } from '../_Models/AuthorAddDTO';
import { Router } from '@angular/router';
import { AuthorService } from '../_Services/AuthorService.service';

@Component({
  selector: 'app-authors-add',
  templateUrl: './authors-add.component.html',
  styleUrls: ['./authors-add.component.css']
})
export class AuthorsAddComponent implements OnInit{
  AddAuthorForm:FormGroup= new FormGroup('');
  done:boolean=false;
  err:string='';
  Author:AuthorAddDTO= new AuthorAddDTO('');

  constructor(
    private router: Router,
    private AuthServ:AuthorService
  ) {}

  ngOnInit(): void {
    this.AddAuthorForm = new FormGroup({
        name: new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(20)])
    })
  }

  onSubmit(){
    this.done = false;
    this.err = '';
    const addData = this.AddAuthorForm.value;
    this.Author = new AuthorAddDTO(addData.name) ;
    console.log(addData);
    if(this.AddAuthorForm.valid){
      this.AuthServ.AddAuthor(this.Author).subscribe(
        (res)=>{
        if(res.status ==200){
            this.done = true;
            this.ngOnInit();
            this.AuthServ.addAuthorCompleted();
        }
        else{
            this.err = "There is an error";
            console.log(res);
        }
        },
        (err)=>{
          if(err.status ==200){
            this.done = true;
            this.ngOnInit();
            this.AuthServ.addAuthorCompleted();
        }
        else{
            this.err = err.error;
            console.log(err);
        }
        }
      )
    }
  }
}
