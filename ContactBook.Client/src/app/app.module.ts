import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material/material.module';
import { ContactsComponent } from './components/contacts/contacts.component';
import { HttpClientModule } from '@angular/common/http';
import { MenuComponent } from './components/menu/menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule } from '@angular/forms';
import { ContactDetailComponent } from './components/contact-detail/contact-detail.component';
import { NewContactComponent } from './components/new-contact/new-contact.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    MenuComponent,
    LoginComponent,
    RegisterComponent,
    ContactDetailComponent,
    NewContactComponent,
    EditContactComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
