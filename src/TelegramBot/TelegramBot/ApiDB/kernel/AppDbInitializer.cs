using ApiDB.Model;
using System.Collections;
namespace ApiDB.dal
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();
                context?.Database.EnsureCreated();
                if (!context.School21.Any())
                {
                    context.School21.AddRange(new List<School21>()
                    {
                        new School21()
                        {

                            CampusName = "Novosibirsk",
                        },
                        new School21()
                        {

                            CampusName = "Moskow",
                        },
                        new School21()
                        {
                            CampusName = "Kazan",
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Subject.Any())
                {
                    context.Subject.AddRange(new List<Subject>() {

                        new Subject()
                        {
                            Campus = 1,
                            NameSubject = "Kitchen",
                            Level = 20,
                            Type = (int)Type.Room,
                            NumberRoom = 1,
                            securType = 1,
                        },
                        new Subject()
                        {
                            Campus = 1,
                            NameSubject = "Shafran holl",
                            Level = 22,
                            Type = (int)Type.Room,
                            securType = 1,

                        },
                        new Subject()
                        {
                            Campus = 1,
                            NameSubject = "Miting room",
                            Level = 20,
                            Type = (int)Type.Room,
                            securType = 0,

                        },
                        new Subject()
                        {
                            Campus = 1,
                            NameSubject = "Play room",
                            Level = 20,
                            Type = (int)Type.Room,
                            securType = 0,

                        },
                        new Subject()
                        {
                            Campus = 1,
                            NameSubject = "Ping-pong",
                            Level = 20,
                            Type = (int)Type.Sport,
                            securType = 0,
                        },

                    });
                    context.SaveChanges();
                }

                if (!context.User.Any())
                {
                    foreach (string line in File.ReadLines(@"..\ApiDB\kernel\Dataset\usersNicks.TXT"))
                    {
                        context.User.Add(new User()
                        {
                            UserLogin = line,
                            Role = (int)UserRole.Student,
                            Campus = (int)Campuses.Novosibirsk,


                        });
                        context.SaveChanges();

                    }
                }
            }
        }
    }
}
