import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { NewsDTO } from '../_Models/NewsDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { NewsService } from '../_Services/NewsService.service';
import { AuthorService } from '../_Services/AuthorService.service';

@Component({
  selector: 'app-news-edit',
  templateUrl: './news-edit.component.html',
  styleUrls: ['./news-edit.component.css']
})
export class NewsEditComponent implements OnInit{
  EditNewsForm:FormGroup = new FormGroup({
    title: new FormControl('',[Validators.required]),
    newsContent:new FormControl('',[Validators.required]),
    publicationDate:new FormControl('',[Validators.required]),
    creationDate:new FormControl(''),
    image:new FormControl(''),
    author:new FormControl('',[Validators.required]),
  });
  NewsToEdit:NewsDTO = new NewsDTO('','','','','','','');
  newsId:string ='';
  authors:AuthorDTO[] = [];
  done:boolean=false;
  err:string='';
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private newsService:NewsService,
    private authorService : AuthorService
  ) {}
ngOnInit(): void {
  this.authorService.getAuthors().subscribe((auths:AuthorDTO[])=>{
    this.authors =auths;
  })
    this.newsId = this.route.snapshot.params['newsId'];
    this.newsService.getNewsById(this.newsId).subscribe((editNews:NewsDTO)=>{
      this.NewsToEdit = editNews;
      this.EditNewsForm.controls['title']?.setValue(this.NewsToEdit.title),
      this.EditNewsForm.controls['author']?.setValue(this.NewsToEdit.authorId),
      this.EditNewsForm.controls['newsContent']?.setValue(this.NewsToEdit.newsContent),
      this.EditNewsForm.controls['publicationDate']?.setValue(this.NewsToEdit.publicationDate),
      this.EditNewsForm.controls['image']?.setValue(this.NewsToEdit.image)
    });
}
selectedFile: File |null = new File([], '');

onFileSelected(event: any) {
  const inputElement = event?.target as HTMLInputElement;
  if (inputElement?.files?.length) {
    this.selectedFile = inputElement.files[0];
  } else {
    this.selectedFile = null;
  }
}
onSubmit(){
  this.done=false;
    this.err = '';
    const toBeEdited =  new NewsDTO('','','','','','','');
    toBeEdited.newsId = this.newsId;
    toBeEdited.creationDate = this.NewsToEdit.creationDate;
    toBeEdited.image = this.NewsToEdit.image;
    toBeEdited.title = this.EditNewsForm?.controls['title']?.value?.toString() as string;
    toBeEdited.newsContent = this.EditNewsForm?.controls['newsContent']?.value;
    toBeEdited.publicationDate = this.EditNewsForm?.controls['publicationDate']?.value?.toString() as string;
    toBeEdited.authorId = this.EditNewsForm?.controls['author']?.value?.toString() as string;
    console.log(toBeEdited);
    console.log(this.selectedFile)
    this.newsService.editNews(this.newsId,toBeEdited,this.selectedFile!).subscribe(
      (res) => {
        console.log(res);
        this.done=true;
        this.router.navigate([`news-details/${this.newsId}`]);
      },
      (err) => {
        console.log(err);
        this.err = err.error;
        if(err.status == 200){
          this.done = true;
        }
        else{
          this.err = err.message;
        }
      }
    )
  }
}
