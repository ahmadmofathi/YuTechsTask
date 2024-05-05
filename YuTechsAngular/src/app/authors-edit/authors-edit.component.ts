import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../_Services/AuthorService.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-authors-edit',
  templateUrl: './authors-edit.component.html',
  styleUrls: ['./authors-edit.component.css']
})
export class AuthorsEditComponent implements OnInit{
  editAuthorForm:FormGroup = new FormGroup({
    name: new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(20)])
  })
  AuthorToEdit:AuthorDTO = new AuthorDTO('','');
  editId:string = ''
  done:boolean=false;
  err:string = '';
  loading:boolean=false;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authorServ:AuthorService
  ) {}


  ngOnInit(): void {
    this.editId =this.route.snapshot.params['author_id'];
    this.authorServ.getAuthorById(this.editId).subscribe((editAuthor:AuthorDTO)=>{
      this.AuthorToEdit = editAuthor;
      this.editAuthorForm.controls['name']?.setValue(this.AuthorToEdit.authorName);
      console.log(this.AuthorToEdit);
    });
  }

  onSubmit(){
    this.done=false;
    this.err = '';
    this.loading = true;
    const toBeEdited =  new AuthorDTO('','');
    toBeEdited.authorId = this.editId;
    toBeEdited.authorName = this.editAuthorForm?.controls['name']?.value?.toString() as string;
    this.authorServ.editAuthor(this.editId,toBeEdited).subscribe(
      (res: HttpResponse<any>) => {
        console.log(res);
        this.done=true;
        this.loading = false;
        this.router.navigate([`authors`]);
      },
      (err) => {
        console.log(err);
        this.err = err.error;
        this.loading = false;
      }
    )
  }
}
