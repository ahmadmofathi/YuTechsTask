import { Component } from '@angular/core';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { Router } from '@angular/router';
import { AuthorService } from '../_Services/AuthorService.service';
import { ConfirmationService,MessageService, SortEvent } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css'],
  providers: [ConfirmationService,DialogService,MessageService]

})
export class AuthorsComponent {
  Authors:AuthorDTO[]=[];
  constructor(
    private router: Router,
    private authorServ:AuthorService,
    private messageService: MessageService,
    public dialogService: DialogService,
    private confirmationService: ConfirmationService,
  ){}

  ngOnInit(): void {
    this.initData();
    this.authorServ.addAuthorObservable.subscribe(() => {
      this.initData();
    });
  }
  initData(){
    this.authorServ.getAuthors().subscribe((authors:AuthorDTO[])=>{
      this.Authors =authors;
    })
  }
  addAuthorCompleted() {
    this.ngOnInit();
  }
  Edit(id:string){
    this.router.navigate([`edit-author/${id}`]);
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
            this.authorServ.deleteAuthor(id).subscribe((res)=>{
                console.log(res);
                this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Record deleted' });
                this.initData();
              },(err)=>{
                this.initData();
              });            
        },
        reject: () => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
        }
    });
}

}

