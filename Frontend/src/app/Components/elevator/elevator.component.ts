import { Component, OnInit, OnDestroy} from '@angular/core';
import { ElevatorService } from '../../Services/elevator.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-elevator',
  templateUrl: './elevator.component.html',
  styleUrls: ['./elevator.component.css']
})
export class ElevatorComponent {
  status: any = {};
  newFloor: number = 1;
  private statusSubscription!: Subscription;

  constructor(private elevatorService: ElevatorService) { }

  ngOnInit(): void {
    this.statusSubscription = this.elevatorService.pollStatus(1000).subscribe(status => {
      this.status = status;
    });
  }

  ngOnDestroy(): void {
    if (this.statusSubscription) {
      this.statusSubscription.unsubscribe();
    }
  }

  refreshStatus() {
    this.elevatorService.getStatus().subscribe(status => this.status = status);
  }

  callElevator() {
    this.elevatorService.callElevator(this.newFloor).subscribe(() => this.refreshStatus());
  }
}
