import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AuthGuard } from './_Auth/auth-guard';
import { LoginComponent } from './login/login.component';
import { NewsComponent } from './news/news.component';
import { AuthorsComponent } from './authors/authors.component';
import { AuthorsAddComponent } from './authors-add/authors-add.component';
import { AuthorsEditComponent } from './authors-edit/authors-edit.component';
import { NewsAddComponent } from './news-add/news-add.component';
import { NewsDetailsComponent } from './news-details/news-details.component';
import { NewsEditComponent } from './news-edit/news-edit.component';

const routes: Routes = [
  { path: '', redirectTo: 'news', pathMatch: 'full' },
  {
    path:'news',
    component: NewsComponent,
  },
  {
    path:'add-news',
    component: NewsAddComponent,
    canActivate:[AuthGuard]
  },
  {
    path:'news-details/:newsId',
    component: NewsDetailsComponent,
  },
  {
    path:'edit-news/:newsId',
    component: NewsEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'admin-panel',
    component: AdminPanelComponent,
    canActivate:[AuthGuard]
  },
  {
    path:'authors',
    component: AuthorsComponent,
    canActivate:[AuthGuard]
  },
  {
    path:'add-author',
    component:AuthorsAddComponent,
    canActivate:[AuthGuard]
  },
  {
    path:'edit-author/:author_id',
    component:AuthorsEditComponent,
    canActivate:[AuthGuard]
  },
  {
    path:'login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
