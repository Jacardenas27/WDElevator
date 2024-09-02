import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ElevatorComponent } from './Components/elevator/elevator.component';

@NgModule({
  declarations: [
    AppComponent,
    ElevatorComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  exports: [
    ElevatorComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
