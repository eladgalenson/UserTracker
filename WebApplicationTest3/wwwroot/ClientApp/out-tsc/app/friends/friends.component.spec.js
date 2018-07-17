"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var friends_component_1 = require("./friends.component");
var friend_component_1 = require("./friend.component");
var http_1 = require("@angular/common/http");
var data_service_1 = require("../../shared/data.service");
var friend_1 = require("../../shared/friend");
var test_utils_1 = require("ClientApp/src/shared/test-utils");
describe('FriendsComponent', function () {
    var getFriendsSpy;
    beforeEach(testing_1.async(function () {
        var dataServiceSpy = jasmine.createSpyObj('DataService', ['getFriends']);
        var friend1 = new friend_1.friend();
        friend1.firstName = "firstName";
        friend1.lastName = "lastName";
        getFriendsSpy = dataServiceSpy.getFriends.and.returnValue(test_utils_1.asyncData([friend1]));
        testing_1.TestBed.configureTestingModule({
            declarations: [
                friends_component_1.FriendsComponent,
                friend_component_1.FriendComponent
            ],
            providers: [
                { provide: data_service_1.DataService, useValue: dataServiceSpy }
            ],
            imports: [http_1.HttpClientModule]
        }).compileComponents();
    }));
    it('should create the friends component', testing_1.async(function () {
        var fixture = testing_1.TestBed.createComponent(friends_component_1.FriendsComponent);
        var fc = fixture.debugElement.componentInstance;
        expect(fc).toBeTruthy();
    }));
    it("should have friends loaded by dataService provider", testing_1.fakeAsync(function () {
        var fixture = testing_1.TestBed.createComponent(friends_component_1.FriendsComponent);
        fixture.detectChanges();
        testing_1.tick();
        fixture.detectChanges(); // update view
        var fc = fixture.debugElement.componentInstance;
        expect(fc.friends.length).toBeGreaterThan(0);
        expect(getFriendsSpy).toHaveBeenCalledTimes(1);
    }));
});
//# sourceMappingURL=friends.component.spec.js.map