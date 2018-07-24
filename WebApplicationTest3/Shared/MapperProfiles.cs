using AutoMapper;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Shared
{
    //When your application runs, Automapper will go through your code looking for classes that inherit from “Profile”, and will load their configuration
    // see https://dotnetcoretutorials.com/2017/09/23/using-automapper-asp-net-core/
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfile, UserProfileViewModel>().
                ForMember(e => e.ImageUpload, opt => opt.Ignore());
            CreateMap<UserProfileViewModel, UserProfile>().
                ForMember(e => e.Id, opt => opt.Ignore()).
                ForMember(e => e.Created, opt => opt.UseValue<DateTime>(DateTime.Now)).
                ForMember(e => e.Modified, opt => opt.UseValue<DateTime>(DateTime.Now));
            //ForMember(vm => vm.ImageUpload, b => b.MapFrom(c => c.ModelName)); ;
        }
    }

    public class UserTrackingProfile : Profile
    {
        public UserTrackingProfile()
        {
            CreateMap<UserOnlinePresence, UserOnlinePresenceViewModel>();
            CreateMap<UserTracking, UserTrackingViewModel>();
        }
    }

    public class UserTrackerProfile : Profile
    {
        public UserTrackerProfile()
        {
            CreateMap<UserTracker, UserTrackerViewModel>();
        }
    }

}
