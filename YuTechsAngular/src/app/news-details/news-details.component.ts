import { Component, OnInit } from '@angular/core';
import { NewsAddDTO } from '../_Models/NewsAddDTO';
import { NewsDTO } from '../_Models/NewsDTO';
import { NewsService } from '../_Services/NewsService.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { AuthorService } from '../_Services/AuthorService.service';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.css']
})
export class NewsDetailsComponent implements OnInit {
news:NewsDTO = new NewsDTO('','','','','','','');
newsId:string ='';

author:AuthorDTO = new AuthorDTO('','');
constructor(
  private newsServ:NewsService,
  private authorServ:AuthorService,
  private router:Router,
  private route: ActivatedRoute,
) {}

ngOnInit(): void {
    this.newsId =this.route.snapshot.params['newsId'];
    this.newsServ.getNewsById(this.newsId).subscribe((n:NewsDTO)=>{
      this.news = n;
      this.authorServ.getAuthorById(n.authorId).subscribe((a:AuthorDTO)=>{
        this.author = a;
      })
    })
}
}
