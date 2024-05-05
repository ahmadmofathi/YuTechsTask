import { Component, OnInit } from '@angular/core';
import { NewsDTO } from '../_Models/NewsDTO';
import { NewsService } from '../_Services/NewsService.service';
import { Router } from '@angular/router';
import { ConfirmationService,MessageService, SortEvent } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css'],
  providers: [ConfirmationService,DialogService,MessageService]
})
export class NewsComponent implements OnInit{
News:NewsDTO[]=[];
  constructor(
    private router: Router,
    private newsServ:NewsService,
    private messageService: MessageService,
    public dialogService: DialogService,
    private confirmationService: ConfirmationService,
  ){}

  userData:any;
  ngOnInit(): void {
    const userDataString = localStorage.getItem('userData');
    this.userData = userDataString ? JSON.parse(userDataString) : null;
    console.log(this.userData);
    
      this.newsServ.getNews().subscribe((news:NewsDTO[])=>{
        this.News =news;
      })
  }
  AddNews(){
    this.router.navigate([`add-news`]);
  }
  
  confirmDelete(event: Event, id: string) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Do you want to delete this record?',
        header: 'Delete Confirmation',
        icon: 'pi pi-info-circle',
        acceptButtonStyleClass:"p-button-danger p-button-text",
        rejectButtonStyleClass:"p-button-text p-button-text",
        acceptIcon:"none",
        rejectIcon:"none",

        accept: () => {
            this.newsServ.deleteNews(id).subscribe((res)=>{
                console.log(res);
                this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Record deleted' });
                this.ngOnInit();
              },(err)=>{
                this.ngOnInit();
              });            
        },
        reject: () => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
        }
    });
}

newsDetails(id:string){
  this.router.navigate([`news-details/${id}`]);
}
EditNews(id:string){
  this.router.navigate([`edit-news/${id}`]);
}

}

