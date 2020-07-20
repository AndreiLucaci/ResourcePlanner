using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Scheduling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using TestScheduler.Converters;
using TestScheduler.Extensions;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        private IOneWayConverter<ExpandoObject, TaskViewModel> _taskConverter = new ExpandoToTaskViewModelConverter();
        private IOneWayConverter<ExpandoObject, UserViewModel> _usrConverter = new ExpandoToUserConverter();

        RowHeightViewModel selectedRowHeight = RowHeightViewModel.Single;

        public virtual UserViewModel UF { get => Users.First(); }
        void SetRowSizes()
        {
            var height = SelectedRowHeight.AppointmentHeight;
            foreach (var usr in Users)
            {
                usr.RowHeight = height;
            }
        }

        public virtual RowHeightViewModel SelectedRowHeight
        {
            get => selectedRowHeight;
            set
            {
                selectedRowHeight = value;
                SetRowSizes();
                RaisePropertyChanged();
            }
        }
        public virtual ObservableCollection<RowHeightViewModel> RowHeights { get; set; } = new ObservableCollection<RowHeightViewModel>
        {
            RowHeightViewModel.Thin,
            RowHeightViewModel.Single,
            RowHeightViewModel.OnePointFive,
            RowHeightViewModel.Double
        };


        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SchedulerViewModel()
        {
            InitializeMockData();
        }

        private void InitializeMockData()
        {
            var res = new List<UserViewModel> {
                CreateUser(1, "Anaé","#47a1c9"),
                CreateUser(2, "Sélène","#8b88b3"),
                CreateUser(3, "Marie-noël","#f905af"),
                CreateUser(4, "Cécilia","#66b4da"),
                CreateUser(5, "Maïté","#1a6ce8"),
                CreateUser(6, "Eléa","#d93766"),
                CreateUser(7, "Léa","#97b96a"),
                CreateUser(8, "Mélissandre","#d7592a"),
                CreateUser(9, "Eugénie","#2798fb"),
                CreateUser(10, "Angèle","#d31d7e"),
                CreateUser(11, "Liè","#67bb29"),
                CreateUser(12, "Åsa","#788e85"),
                CreateUser(13, "Mélia","#c98842"),
                CreateUser(14, "Gaïa","#752dfb"),
                CreateUser(15, "Josée","#107ee9"),
                CreateUser(16, "Marylène","#41fb65"),
                CreateUser(17, "Personnalisée","#0daaa7"),
                CreateUser(18, "Pål","#c45a29"),
                CreateUser(19, "Loïca","#e7e782"),
                CreateUser(20, "Léa","#f6dce6")
            };

            Users = new ObservableCollection<UserViewModel>(res.GroupBy(x => x.Department).OrderBy(x => x.Key).SelectMany(x => x).ToArray());

            var itms = new List<TaskViewModel>{
                CreateTask( 1, UnixTimeStampToDateTime(1594624987d), UnixTimeStampToDateTime(1595746824d), 37,"Kitra", "velit. Cras lorem lorem, luctus ut, pellentesque", 7),
                CreateTask( 2, UnixTimeStampToDateTime(1596263722d), UnixTimeStampToDateTime(1596132460d), 72,"Indigo", "Quisque ac libero nec ligula consectetuer rhoncus. Nullam velit", 12),
                CreateTask( 3, UnixTimeStampToDateTime(1594973979d), UnixTimeStampToDateTime(1595897249d), 70,"Sybill", "mi. Aliquam gravida mauris ut mi.", 20),
                CreateTask( 4, UnixTimeStampToDateTime(1594796375d), UnixTimeStampToDateTime(1594716113d), 83,"Gisela", "Cras convallis convallis dolor. Quisque tincidunt pede", 3),
                CreateTask( 5, UnixTimeStampToDateTime(1594353997d), UnixTimeStampToDateTime(1595263678d), 3, "Zephania","rutrum lorem ac risus. Morbi metus. Vivamus", 9),
                CreateTask( 6, UnixTimeStampToDateTime(1595018238d), UnixTimeStampToDateTime(1595078204d), 16,"John", "gravida mauris ut mi. Duis risus odio, auctor", 1),
                CreateTask( 7, UnixTimeStampToDateTime(1594530417d), UnixTimeStampToDateTime(1596026823d), 16,"Bethany", "eros. Proin ultrices. Duis volutpat nunc sit amet metus.", 17),
                CreateTask( 8, UnixTimeStampToDateTime(1594306651d), UnixTimeStampToDateTime(1595311225d), 84,"Lionel", "Nullam velit dui, semper et, lacinia vitae, sodales at,", 14),
                CreateTask( 9, UnixTimeStampToDateTime(1595371013d), UnixTimeStampToDateTime(1595940453d), 91,"Boris", "tincidunt tempus risus. Donec egestas. Duis", 11),
                CreateTask(10, UnixTimeStampToDateTime(1594630661d), UnixTimeStampToDateTime(1595187262d), 24,"Kitra", "magna nec quam. Curabitur vel lectus. Cum", 20),
                CreateTask(11, UnixTimeStampToDateTime(1596084741d), UnixTimeStampToDateTime(1594625314d), 51,"Erich", "Cras vulputate velit eu sem. Pellentesque ut ipsum ac mi", 6),
                CreateTask(12, UnixTimeStampToDateTime(1594427651d), UnixTimeStampToDateTime(1594800889d), 29,"Karen", "ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis", 4),
                CreateTask(13, UnixTimeStampToDateTime(1595930439d), UnixTimeStampToDateTime(1594454449d), 5, "Keegan","eros turpis non enim. Mauris quis", 13),
                CreateTask(14, UnixTimeStampToDateTime(1595176256d), UnixTimeStampToDateTime(1594572287d), 25,"Ferdinand", "adipiscing ligula. Aenean gravida nunc sed pede. Cum", 15),
                CreateTask(15, UnixTimeStampToDateTime(1596049217d), UnixTimeStampToDateTime(1595043349d), 36,"Zelenia", "orci luctus et ultrices posuere cubilia Curae; Phasellus ornare.", 14),
                CreateTask(16, UnixTimeStampToDateTime(1595550261d), UnixTimeStampToDateTime(1594869295d), 89,"Aquila", "ut ipsum ac mi eleifend egestas.", 11),
                CreateTask(17, UnixTimeStampToDateTime(1594651981d), UnixTimeStampToDateTime(1595501754d), 23,"Jennifer", "vitae aliquam eros turpis non enim. Mauris quis turpis vitae", 20),
                CreateTask(18, UnixTimeStampToDateTime(1595090763d), UnixTimeStampToDateTime(1595995999d), 23,"Macon", "rutrum non, hendrerit id, ante. Nunc mauris sapien, cursus", 2),
                CreateTask(19, UnixTimeStampToDateTime(1596002211d), UnixTimeStampToDateTime(1594648457d), 94,"Walker", "quis, tristique ac, eleifend vitae, erat. Vivamus nisi. Mauris nulla.", 17),
                CreateTask(20, UnixTimeStampToDateTime(1594475165d), UnixTimeStampToDateTime(1595166773d), 30,"Nerea", "Fusce aliquet magna a neque. Nullam", 12),
                CreateTask(21, UnixTimeStampToDateTime(1595691594d), UnixTimeStampToDateTime(1594602138d), 14,"Walter", "nunc sit amet metus. Aliquam erat volutpat. Nulla facilisis.", 19),
                CreateTask(22, UnixTimeStampToDateTime(1595451350d), UnixTimeStampToDateTime(1595044058d), 27,"Finn", "pretium neque. Morbi quis urna. Nunc", 19),
                CreateTask(23, UnixTimeStampToDateTime(1594403030d), UnixTimeStampToDateTime(1596241691d), 44,"Elton", "Nullam velit dui, semper et, lacinia", 17),
                CreateTask(24, UnixTimeStampToDateTime(1595668195d), UnixTimeStampToDateTime(1595986313d), 83,"Wayne", "augue. Sed molestie. Sed id risus quis diam luctus lobortis.", 3),
                CreateTask(25, UnixTimeStampToDateTime(1595642068d), UnixTimeStampToDateTime(1595991685d), 75,"Fitzgerald", "fermentum risus, at fringilla purus mauris a nunc.", 3),
                CreateTask(26, UnixTimeStampToDateTime(1595655593d), UnixTimeStampToDateTime(1594374072d), 56,"Reese", "penatibus et magnis dis parturient montes, nascetur ridiculus", 18),
                CreateTask(27, UnixTimeStampToDateTime(1594672338d), UnixTimeStampToDateTime(1595648831d), 74,"Geoffrey", "adipiscing. Mauris molestie pharetra nibh. Aliquam ornare, libero at", 19),
                CreateTask(28, UnixTimeStampToDateTime(1595381036d), UnixTimeStampToDateTime(1594538579d), 75,"Cedric", "suscipit, est ac facilisis facilisis, magna tellus faucibus leo,", 10),
                CreateTask(29, UnixTimeStampToDateTime(1596252682d), UnixTimeStampToDateTime(1596080081d), 42,"Aphrodite", "ornare sagittis felis. Donec tempor, est ac mattis", 5),
                CreateTask(30, UnixTimeStampToDateTime(1594884106d), UnixTimeStampToDateTime(1594713183d), 5, "Veronica","consectetuer, cursus et, magna. Praesent interdum ligula eu enim. Etiam", 19),
                CreateTask(31, UnixTimeStampToDateTime(1595452645d), UnixTimeStampToDateTime(1596264332d), 52,"Rigel", "nunc. Quisque ornare tortor at risus. Nunc ac sem ut", 9),
                CreateTask(32, UnixTimeStampToDateTime(1595209204d), UnixTimeStampToDateTime(1595484805d), 75,"Bevis", "in, tempus eu, ligula. Aenean euismod mauris eu", 9),
                CreateTask(33, UnixTimeStampToDateTime(1596139364d), UnixTimeStampToDateTime(1595369630d), 77,"Silas", "volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh. Phasellus nulla.", 16),
                CreateTask(34, UnixTimeStampToDateTime(1594787834d), UnixTimeStampToDateTime(1595854861d), 68,"Bertha", "rutrum eu, ultrices sit amet, risus. Donec nibh", 3),
                CreateTask(35, UnixTimeStampToDateTime(1595565578d), UnixTimeStampToDateTime(1595442713d), 41,"Bevis", "dui, in sodales elit erat vitae", 9),
                CreateTask(36, UnixTimeStampToDateTime(1596194866d), UnixTimeStampToDateTime(1595848964d), 46,"Edward", "dolor. Donec fringilla. Donec feugiat metus sit amet", 20),
                CreateTask(37, UnixTimeStampToDateTime(1594692790d), UnixTimeStampToDateTime(1595765185d), 60,"Wayne", "a, arcu. Sed et libero. Proin mi. Aliquam gravida mauris", 3),
                CreateTask(38, UnixTimeStampToDateTime(1595548136d), UnixTimeStampToDateTime(1595614821d), 26,"Jaquelyn", "facilisis vitae, orci. Phasellus dapibus quam quis diam. Pellentesque", 8),
                CreateTask(39, UnixTimeStampToDateTime(1595532611d), UnixTimeStampToDateTime(1595401229d), 86,"Edward", "tortor. Integer aliquam adipiscing lacus. Ut nec", 18),
                CreateTask(40, UnixTimeStampToDateTime(1594659393d), UnixTimeStampToDateTime(1594615655d), 59,"Shelby", "urna. Nullam lobortis quam a felis ullamcorper viverra.", 8),
                CreateTask(41, UnixTimeStampToDateTime(1595352989d), UnixTimeStampToDateTime(1595738975d), 5, "Emma","Class aptent taciti sociosqu ad litora", 4),
                CreateTask(42, UnixTimeStampToDateTime(1595271442d), UnixTimeStampToDateTime(1595509956d), 84,"Nora", "mauris sapien, cursus in, hendrerit consectetuer, cursus et,", 14),
                CreateTask(43, UnixTimeStampToDateTime(1595539383d), UnixTimeStampToDateTime(1594279928d), 74,"James", "velit dui, semper et, lacinia vitae, sodales", 19),
                CreateTask(44, UnixTimeStampToDateTime(1594441606d), UnixTimeStampToDateTime(1594574311d), 88,"Brenden", "penatibus et magnis dis parturient montes, nascetur", 6),
                CreateTask(45, UnixTimeStampToDateTime(1596147574d), UnixTimeStampToDateTime(1596186370d), 23,"Katelyn", "sapien, cursus in, hendrerit consectetuer, cursus", 19),
                CreateTask(46, UnixTimeStampToDateTime(1596005831d), UnixTimeStampToDateTime(1594341335d), 35,"Cyrus", "libero et tristique pellentesque, tellus sem mollis dui,", 7),
                CreateTask(47, UnixTimeStampToDateTime(1596016773d), UnixTimeStampToDateTime(1594483136d), 41,"Macon", "Nunc lectus pede, ultrices a, auctor non, feugiat nec,", 2),
                CreateTask(48, UnixTimeStampToDateTime(1594610497d), UnixTimeStampToDateTime(1595171018d), 41,"Trevor", "libero. Morbi accumsan laoreet ipsum. Curabitur", 12),
                CreateTask(49, UnixTimeStampToDateTime(1595303653d), UnixTimeStampToDateTime(1595273973d), 40,"Tanner", "tempus eu, ligula. Aenean euismod mauris eu", 4),
                CreateTask(50, UnixTimeStampToDateTime(1594569976d), UnixTimeStampToDateTime(1595435529d), 43,"Abra", "viverra. Donec tempus, lorem fringilla ornare placerat, orci lacus", 19),
                CreateTask(51, UnixTimeStampToDateTime(1594371492d), UnixTimeStampToDateTime(1595555915d), 81,"Graham", "Phasellus in felis. Nulla tempor augue ac ipsum. Phasellus vitae", 13),
                CreateTask(52, UnixTimeStampToDateTime(1594848479d), UnixTimeStampToDateTime(1594525344d), 22,"Cailin", "at pede. Cras vulputate velit eu sem. Pellentesque", 19),
                CreateTask(53, UnixTimeStampToDateTime(1594308562d), UnixTimeStampToDateTime(1594344973d), 43,"Walter", "libero lacus, varius et, euismod et, commodo", 5),
                CreateTask(54, UnixTimeStampToDateTime(1594922919d), UnixTimeStampToDateTime(1595710253d), 84,"Peter", "ligula. Aliquam erat volutpat. Nulla dignissim. Maecenas ornare egestas ligula.", 4),
                CreateTask(55, UnixTimeStampToDateTime(1594285229d), UnixTimeStampToDateTime(1595248408d), 12,"Vielka", "Fusce fermentum fermentum arcu. Vestibulum ante ipsum primis in", 10),
                CreateTask(56, UnixTimeStampToDateTime(1595921998d), UnixTimeStampToDateTime(1595691718d), 28,"Blake", "Integer vulputate, risus a ultricies adipiscing, enim", 11),
                CreateTask(57, UnixTimeStampToDateTime(1595226322d), UnixTimeStampToDateTime(1595109356d), 67,"Yen", "rutrum non, hendrerit id, ante. Nunc mauris sapien, cursus", 4),
                CreateTask(58, UnixTimeStampToDateTime(1595106557d), UnixTimeStampToDateTime(1595581304d), 43,"Alvin", "Donec nibh enim, gravida sit amet, dapibus", 4),
                CreateTask(59, UnixTimeStampToDateTime(1595165450d), UnixTimeStampToDateTime(1594853583d), 16,"Eve", "ipsum primis in faucibus orci luctus et ultrices posuere", 18),
                CreateTask(60, UnixTimeStampToDateTime(1594284050d), UnixTimeStampToDateTime(1595905480d), 36,"Carla", "eget, ipsum. Donec sollicitudin adipiscing ligula. Aenean gravida", 2),
                CreateTask(61, UnixTimeStampToDateTime(1595500206d), UnixTimeStampToDateTime(1594624966d), 47,"Felix", "Quisque fringilla euismod enim. Etiam gravida molestie arcu. Sed", 6),
                CreateTask(62, UnixTimeStampToDateTime(1596057919d), UnixTimeStampToDateTime(1594411657d), 82,"Macey", "mauris sapien, cursus in, hendrerit consectetuer, cursus et, magna.", 4),
                CreateTask(63, UnixTimeStampToDateTime(1595141036d), UnixTimeStampToDateTime(1594767055d), 77,"Cecilia", "nec, imperdiet nec, leo. Morbi neque tellus, imperdiet non, vestibulum", 1),
                CreateTask(64, UnixTimeStampToDateTime(1594304934d), UnixTimeStampToDateTime(1594364307d), 92,"Todd", "In at pede. Cras vulputate velit eu sem. Pellentesque", 14),
                CreateTask(65, UnixTimeStampToDateTime(1595377484d), UnixTimeStampToDateTime(1595905789d), 38,"Aladdin", "sociis natoque penatibus et magnis dis parturient", 6),
                CreateTask(66, UnixTimeStampToDateTime(1595455465d), UnixTimeStampToDateTime(1595718263d), 84,"Hector", "dis parturient montes, nascetur ridiculus mus.", 10),
                CreateTask(67, UnixTimeStampToDateTime(1594961583d), UnixTimeStampToDateTime(1596045429d), 31,"Brandon", "interdum. Curabitur dictum. Phasellus in felis. Nulla tempor", 3),
                CreateTask(68, UnixTimeStampToDateTime(1595320618d), UnixTimeStampToDateTime(1595664643d), 44,"Kathleen", "Donec elementum, lorem ut aliquam iaculis, lacus pede sagittis augue,", 14),
                CreateTask(69, UnixTimeStampToDateTime(1594902337d), UnixTimeStampToDateTime(1595319823d), 70,"Hamish", "neque. Nullam nisl. Maecenas malesuada fringilla est. Mauris", 4),
                CreateTask(70, UnixTimeStampToDateTime(1596232139d), UnixTimeStampToDateTime(1594677335d), 90,"Cody", "quis massa. Mauris vestibulum, neque sed dictum eleifend, nunc risus", 17),
                CreateTask(71, UnixTimeStampToDateTime(1596126362d), UnixTimeStampToDateTime(1594627659d), 21,"Kennan", "sem magna nec quam. Curabitur vel lectus.", 18),
                CreateTask(72, UnixTimeStampToDateTime(1595075923d), UnixTimeStampToDateTime(1594327818d), 73,"Ross", "adipiscing lobortis risus. In mi pede, nonummy ut, molestie", 8),
                CreateTask(73, UnixTimeStampToDateTime(1595883871d), UnixTimeStampToDateTime(1594943556d), 7, "Deirdre","vulputate, risus a ultricies adipiscing, enim mi", 8),
                CreateTask(74, UnixTimeStampToDateTime(1596025527d), UnixTimeStampToDateTime(1595885177d), 5, "Carissa","Phasellus vitae mauris sit amet lorem semper auctor.", 1),
                CreateTask(75, UnixTimeStampToDateTime(1594396368d), UnixTimeStampToDateTime(1595674105d), 67,"Jared", "id, blandit at, nisi. Cum sociis", 7),
                CreateTask(76, UnixTimeStampToDateTime(1594427552d), UnixTimeStampToDateTime(1596097114d), 48,"Raphael", "arcu. Nunc mauris. Morbi non sapien molestie", 20),
                CreateTask(77, UnixTimeStampToDateTime(1594622170d), UnixTimeStampToDateTime(1596254898d), 11,"Mohammad", "vitae risus. Duis a mi fringilla mi", 7),
                CreateTask(78, UnixTimeStampToDateTime(1596203281d), UnixTimeStampToDateTime(1594939187d), 29,"Jana", "suscipit, est ac facilisis facilisis, magna tellus faucibus", 18),
                CreateTask(79, UnixTimeStampToDateTime(1595444413d), UnixTimeStampToDateTime(1594360751d), 51,"Jayme", "luctus et ultrices posuere cubilia Curae; Donec tincidunt.", 10),
                CreateTask(80, UnixTimeStampToDateTime(1596187907d), UnixTimeStampToDateTime(1594530678d), 36,"Ezra", "diam nunc, ullamcorper eu, euismod ac, fermentum", 4),
                CreateTask(81, UnixTimeStampToDateTime(1594413590d), UnixTimeStampToDateTime(1595252898d), 4, "Mira","gravida non, sollicitudin a, malesuada id, erat. Etiam vestibulum", 1),
                CreateTask(82, UnixTimeStampToDateTime(1595025759d), UnixTimeStampToDateTime(1595939271d), 49,"Dane", "Proin nisl sem, consequat nec, mollis vitae, posuere at, velit.", 10),
                CreateTask(83, UnixTimeStampToDateTime(1596008359d), UnixTimeStampToDateTime(1595836480d), 94,"Mari", "fringilla ornare placerat, orci lacus vestibulum", 6),
                CreateTask(84, UnixTimeStampToDateTime(1594963983d), UnixTimeStampToDateTime(1595693893d), 54,"Jasper", "et ultrices posuere cubilia Curae; Donec", 15),
                CreateTask(85, UnixTimeStampToDateTime(1595617082d), UnixTimeStampToDateTime(1595022212d), 91,"Flynn", "Nullam velit dui, semper et, lacinia vitae, sodales at,", 11),
                CreateTask(86, UnixTimeStampToDateTime(1595892772d), UnixTimeStampToDateTime(1596120780d), 3, "Jacob","vel quam dignissim pharetra. Nam ac nulla. In tincidunt congue", 8),
                CreateTask(87, UnixTimeStampToDateTime(1595080529d), UnixTimeStampToDateTime(1596010876d), 81,"Xena", "odio sagittis semper. Nam tempor diam", 7),
                CreateTask(88, UnixTimeStampToDateTime(1595180770d), UnixTimeStampToDateTime(1595332256d), 76,"Philip", "Duis at lacus. Quisque purus sapien, gravida non,", 11),
                CreateTask(89, UnixTimeStampToDateTime(1595492906d), UnixTimeStampToDateTime(1595995893d), 10,"Axel", "mattis. Cras eget nisi dictum augue malesuada", 6),
                CreateTask(90, UnixTimeStampToDateTime(1595337080d), UnixTimeStampToDateTime(1595004266d), 21,"Kim", "luctus, ipsum leo elementum sem, vitae", 14),
                CreateTask(91, UnixTimeStampToDateTime(1595632713d), UnixTimeStampToDateTime(1595514425d), 19,"Castor", "non justo. Proin non massa non ante bibendum ullamcorper. Duis", 8),
                CreateTask(92, UnixTimeStampToDateTime(1594792540d), UnixTimeStampToDateTime(1595699144d), 93,"Randall", "dignissim. Maecenas ornare egestas ligula. Nullam feugiat placerat", 10),
                CreateTask(93, UnixTimeStampToDateTime(1595784855d), UnixTimeStampToDateTime(1595883540d), 76,"Alfonso", "nisi nibh lacinia orci, consectetuer euismod est", 17),
                CreateTask(94, UnixTimeStampToDateTime(1594375217d), UnixTimeStampToDateTime(1595998202d), 83,"Quentin", "ultrices. Vivamus rhoncus. Donec est. Nunc ullamcorper, velit in", 3),
                CreateTask(95, UnixTimeStampToDateTime(1595907481d), UnixTimeStampToDateTime(1594663913d), 55,"Kalia", "blandit congue. In scelerisque scelerisque dui. Suspendisse ac", 2),
                CreateTask(96, UnixTimeStampToDateTime(1594279158d), UnixTimeStampToDateTime(1594393084d), 58,"Dorothy", "Etiam laoreet, libero et tristique pellentesque, tellus sem mollis dui,", 9),
                CreateTask(97, UnixTimeStampToDateTime(1594308607d), UnixTimeStampToDateTime(1596119543d), 29,"Shelly", "eu, eleifend nec, malesuada ut, sem. Nulla", 6),
                CreateTask(98, UnixTimeStampToDateTime(1596214865d), UnixTimeStampToDateTime(1594367536d), 53,"Lee", "ut erat. Sed nunc est, mollis non, cursus", 1),
                CreateTask(99, UnixTimeStampToDateTime(1595690091d), UnixTimeStampToDateTime(1594347552d), 6, "Ross","scelerisque neque sed sem egestas blandit. Nam nulla", 17),
                CreateTask(100, UnixTimeStampToDateTime(1594376094d), UnixTimeStampToDateTime(1594738517d),97,"Lara", "enim consequat purus. Maecenas libero est, congue a, aliquet", 2)
            };

            var itmsGrouped = new List<TaskViewModel>();

            AllTasks = new ObservableCollection<TaskViewModel>(itms);
            SelectedTasks = new ObservableCollection<TaskViewModel>(itms);
            var converter = new UserToResourceItemConverter();
            UsersRes = new ObservableCollection<ResourceItem>(Users.Select(converter.Convert));
        }


        public virtual ObservableCollection<TaskViewModel> SelectedTasks { get; set; } = new ObservableCollection<TaskViewModel>();
        public virtual ObservableCollection<TaskViewModel> AllTasks { get; set; } = new ObservableCollection<TaskViewModel>();
        public virtual ObservableCollection<UserViewModel> Users { get; set; } = new ObservableCollection<UserViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        internal void RefreshTimeFrameSelection()
        {
            switch (SelectedTimeFrame.TimeFrame)
            {
                case ViewModels.TimeFrames.Daily:
                    SetTimeFrameDaily();
                    break;
                case ViewModels.TimeFrames.Weekly:
                    SetTimeFrameWeekly();
                    break;
                case ViewModels.TimeFrames.BiWeekly:
                    SetTimeFrameBiWeekly();
                    break;
                case ViewModels.TimeFrames.Monthly:
                    SetTimeFrameMonthly();
                    break;
                case ViewModels.TimeFrames.Yearly:
                    SetTimeFrameYearly();
                    break;
            }
        }
        void SetTimeFrameDaily()
        {
            TimeScale.TimeScaleDay = false;
            TimeScale.TimeScaleHour = false;
            TimeScale.TimeScaleMonth = false;
            TimeScale.TimeScaleQuarter = false;
            TimeScale.TimeScaleWeek = true;
            TimeScale.TimeScaleWorkDay = true;
            TimeScale.TimeScaleWorkHour = true;
            TimeScale.TimeScaleYear = false;
            RaisePropertyChanged(nameof(TimeScale));
        }

        void SetTimeFrameWeekly()
        {
            TimeScale.TimeScaleDay = false;
            TimeScale.TimeScaleHour = false;
            TimeScale.TimeScaleMonth = true;
            TimeScale.TimeScaleQuarter = false;
            TimeScale.TimeScaleWeek = true;
            TimeScale.TimeScaleWorkDay = false;
            TimeScale.TimeScaleWorkHour = false;
            TimeScale.TimeScaleYear = true;
            RaisePropertyChanged(nameof(TimeScale));
        }

        void SetTimeFrameBiWeekly()
        {
            TimeScale.TimeScaleDay = false;
            TimeScale.TimeScaleHour = false;
            TimeScale.TimeScaleMonth = true;
            TimeScale.TimeScaleQuarter = true;
            TimeScale.TimeScaleWeek = true;
            TimeScale.TimeScaleWorkDay = false;
            TimeScale.TimeScaleWorkHour = false;
            TimeScale.TimeScaleYear = true;
            RaisePropertyChanged(nameof(TimeScale));
        }

        void SetTimeFrameMonthly()
        {
            TimeScale.TimeScaleDay = false;
            TimeScale.TimeScaleHour = false;
            TimeScale.TimeScaleMonth = true;
            TimeScale.TimeScaleQuarter = true;
            TimeScale.TimeScaleWeek = false;
            TimeScale.TimeScaleWorkDay = false;
            TimeScale.TimeScaleWorkHour = false;
            TimeScale.TimeScaleYear = true;
            RaisePropertyChanged(nameof(TimeScale));
        }

        void SetTimeFrameYearly()
        {
            TimeScale.TimeScaleDay = false;
            TimeScale.TimeScaleHour = false;
            TimeScale.TimeScaleMonth = false;
            TimeScale.TimeScaleQuarter = false;
            TimeScale.TimeScaleWeek = false;
            TimeScale.TimeScaleWorkDay = false;
            TimeScale.TimeScaleWorkHour = false;
            TimeScale.TimeScaleYear = true;
            RaisePropertyChanged(nameof(TimeScale));
        }

        public virtual TimeScaleViewModel TimeScale { get; set; } = new TimeScaleViewModel();

        public virtual TimeFrameViewModel SelectedTimeFrame { get; set; } = TimeFrameViewModel.Daily;

        public virtual ObservableCollection<TimeFrameViewModel> TimeFrames { get; set; } = new ObservableCollection<TimeFrameViewModel>
        {
            TimeFrameViewModel.Daily,
            TimeFrameViewModel.Weekly,
            TimeFrameViewModel.BiWeekly,
            TimeFrameViewModel.Monthly,
            TimeFrameViewModel.Yearly,
        };
        public virtual ObservableCollection<ResourceItem> UsersRes { get; set; }

        private UserViewModel CreateUser(int id, string name, string color)
        {
            var rnd = new Random();
            dynamic itm = new ExpandoObject();
            itm.Id = id; // + DateTime.Now.Millisecond;
            itm.Name = name;
            itm.Color = color;
            itm.Department = (DateTime.Now.Millisecond % 2 == 0) ? "USR" : "IT";

            var usr = _usrConverter.Convert(itm);
            usr.RowHeight = rnd.Next(SelectedRowHeight.AppointmentHeight - 5, SelectedRowHeight.AppointmentHeight + 15);
            return usr;
        }

        private TaskViewModel CreateTask(int id, DateTime startTime, DateTime finishTime, int progress, string name, string tooltip, int resId)
        {
            dynamic itm = new ExpandoObject();
            itm.StartDateTime = startTime;
            itm.FinishDateTime = finishTime;
            itm.ShownText = name;
            itm.ToolTipText = tooltip;
            itm.Completed = progress;
            itm.Id = id + DateTime.Now.Millisecond;

            TaskViewModel res = _taskConverter.Convert(itm);
            res.UserId = resId;
            //res.ResourceLinks = new System.Collections.ObjectModel.ObservableCollection<GanttResourceLink> {
            //    new GanttResourceLink {
            //        ResourceId = resId,
            //        AllocationPercentage = progress
            //    }
            //};

            return res;
        }

        public void CollapseRows(IEnumerable<int> ids)
        {
            SelectedTasks.RemoveAll(x => ids.Contains(x.UserId));
        }

        public void ExpandRows(IEnumerable<int> ids)
        {
            //AllTasks.Where(x => ids.Contains(x.UserId)
            //SelectedTasks.Where(x => x.UserId == userId).ToList().ForEach(x => SelectedTasks.Remove(x));
        }
    }
}