import { Component, OnInit } from '@angular/core';
import { NewsService } from '../_Services/NewsService.service';
import { NewsAddDTO } from '../_Models/NewsAddDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { AuthorService } from '../_Services/AuthorService.service';

@Component({
  selector: 'app-news-add',
  templateUrl: './news-add.component.html',
  styleUrls: ['./news-add.component.css']
})
export class NewsAddComponent implements OnInit{
  AddNewsForm:FormGroup = new FormGroup('');
  authors:AuthorDTO[] = [];
  news:NewsAddDTO= new NewsAddDTO('','','','','','');
  done:boolean=false;
  err:string='';
  constructor(
    private router: Router,
    private newsService:NewsService,
    private authorService : AuthorService
  ) {}
  ngOnInit(): void {
    this.authorService.getAuthors().subscribe((auths:AuthorDTO[])=>{
      this.authors = auths;
      console.log(auths);
    })
    this.AddNewsForm = new FormGroup({
      title: new FormControl('',[Validators.required]),
      author: new FormControl('',[Validators.required]),
      content: new FormControl('',[Validators.required]),
      image: new FormControl(''),
      publicationDate: new FormControl('',[Validators.required]),
      creationDate: new FormControl(''),
    })
  }
  onSubmit() {
    this.done = false;
    this.err = '';
    const addData = this.AddNewsForm.value;
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const day = String(today.getDate()).padStart(2, '0');

const formattedDate = `${year}-${month}-${day}`;
    this.news = new NewsAddDTO(
      this.AddNewsForm.controls['title'].value,
      this.AddNewsForm.controls['content'].value,
      this.AddNewsForm.controls['title'].value,
      this.AddNewsForm.controls['publicationDate'].value?.toString() as string,
      formattedDate,
      this.AddNewsForm.controls['author'].value,
    )
    console.log(this.news);
    console.log(this.selectedFile);
    this.newsService.AddNews(this.news, this.selectedFile!).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
        if(error.status == 200){
          this.done = true;
        }
        else{
          this.err = error.message;
        }
      }
    );
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
  
}
