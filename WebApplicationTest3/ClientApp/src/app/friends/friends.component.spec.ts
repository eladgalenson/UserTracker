import { TestBed, async, fakeAsync, tick } from '@angular/core/testing';
import { FriendsComponent } from './friends.component';
import { FriendComponent } from './friend.component';
import { HttpClientModule } from '@angular/common/http';
import { DataService } from '../../shared/data.service';
import { friend } from '../../shared/friend';
import { of } from 'rxjs';
import { asyncData } from 'ClientApp/src/shared/test-utils';



describe('FriendsComponent', () => {

    let getFriendsSpy: Partial<DataService>;
    
    beforeEach(async(() => {

        const dataServiceSpy = jasmine.createSpyObj('DataService', ['getFriends']);
        let friend1 = new friend();
        friend1.firstName = "firstName";
        friend1.lastName = "lastName";
        getFriendsSpy = dataServiceSpy.getFriends.and.returnValue(asyncData([friend1]));


        TestBed.configureTestingModule({
            declarations: [
                FriendsComponent,
                FriendComponent
            ],
            providers: [
                { provide: DataService, useValue: dataServiceSpy }
            ],
            imports: [HttpClientModule]
        }).compileComponents();
    }));
    it('should create the friends component', async(() => {
        const fixture = TestBed.createComponent(FriendsComponent);
        const fc = fixture.debugElement.componentInstance;
        expect(fc).toBeTruthy();
    }));
    it(`should have friends loaded by dataService provider`, fakeAsync(() => {
        const fixture = TestBed.createComponent(FriendsComponent);
        
        fixture.detectChanges();
        tick();
        fixture.detectChanges(); // update view
        const fc = fixture.debugElement.componentInstance;
        expect((<friend[]>fc.friends).length).toBeGreaterThan(0);
        expect(getFriendsSpy).toHaveBeenCalledTimes(1);
    }));
});