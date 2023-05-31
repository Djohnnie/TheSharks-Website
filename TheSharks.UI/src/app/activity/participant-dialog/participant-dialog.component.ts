import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Participant } from 'src/types/application/activity/Participant';
import { OpenWaterTestContentDialogComponent } from '../../open-water-test/open-water-test-content-dialog/open-water-test-content-dialog.component'

@Component({
  selector: 'app-participant-dialog',
  templateUrl: './participant-dialog.component.html',
  styleUrls: ['./participant-dialog.component.scss']
})
export class ParticipantDialogComponent implements OnInit {

  participant: Participant
  isDiveActivity: boolean

  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    public dialog: MatDialog) { 
    this.participant = data.participant
    this.isDiveActivity = data.isDiveActivity
  }

  ngOnInit(): void {
  }

  openContentDialog() {
    this.dialog.open(OpenWaterTestContentDialogComponent, {
        data: { 
          title: this.participant.openWaterTestTitle,
          content: this.participant.openWaterTestContent
        }
    })
  }

}