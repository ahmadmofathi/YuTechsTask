import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 

// PrimeNg Imports
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { ImageModule } from 'primeng/image';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NewsComponent } from './news/news.component';
import { AuthorsComponent } from './authors/authors.component';
import { AuthorsAddComponent } from './authors-add/authors-add.component';
import { AuthorsEditComponent } from './authors-edit/authors-edit.component';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NewsAddComponent } from './news-add/news-add.component';
import { NewsEditComponent } from './news-edit/news-edit.component';
import { NewsDetailsComponent } from './news-details/news-details.component';


@NgModule({
  declarations: [
    AppComponent,
    AdminPanelComponent,
    LoginComponent,
    NavbarComponent,
    NewsComponent,
    AuthorsComponent,
    AuthorsAddComponent,
    AuthorsEditComponent,
    NewsAddComponent,
    NewsEditComponent,
    NewsDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CheckboxModule,
    InputTextModule,
    ButtonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    ConfirmDialogModule,
    ToastModule,
    ConfirmPopupModule,
    BrowserAnimationsModule,
    ImageModule,
    FileUploadModule,
    InputTextareaModule,
    DropdownModule
  ],
  providers: [ConfirmationService,DialogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
