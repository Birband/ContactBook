import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ContactDetailComponent } from './components/contact-detail/contact-detail.component';
import { NewContactComponent } from './components/new-contact/new-contact.component';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';

const routes: Routes = [
  { path: 'contacts', component: ContactsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'contact/:email', component: ContactDetailComponent },
  { path: 'new-contact', component: NewContactComponent },
  { path: 'edit-contact/:email', component: EditContactComponent },
  { path: '', redirectTo: '/contacts', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
