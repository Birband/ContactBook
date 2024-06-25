import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from '../../services/api/api.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css'
})
export class ContactsComponent implements OnInit {
  isLogged = false;  
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phoneNumber'];

  dataSource = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private apiService: ApiService, 
              private authService: AuthService,
              private router: Router) { }

  ngOnInit(): void {
    this.authService.isLoggedIn$().subscribe((isLoggedIn) => {
      this.isLogged = isLoggedIn;
      if (isLoggedIn)
      {
        this.displayedColumns.push('actions');
      } else {
        this.displayedColumns = ['firstName', 'lastName', 'email', 'phoneNumber'];
      }
    });
    this.fetchContacts();
  }

  fetchContacts() {
    this.apiService.fetchContacts().subscribe((data: any[]) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    },
    (error) => {
      console.error('Error fetching contacts: ', error);
    });
  }

  viewContactDetail(email: string): void {
    this.router.navigate(['/contact', email]);
  }

  deleteContact(email: string): void {
    this.apiService.deleteContact(email).subscribe(() => {
      this.router.navigate(['/contacts']);
    },
    (error) => {
      console.error('Error deleting contact: ', error);
    });
  }

  editContact(email: string): void {
    this.router.navigate(['/edit-contact', email]);
  }
}
