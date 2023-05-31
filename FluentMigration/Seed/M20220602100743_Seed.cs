using FluentMigrator;
using FluentMigrator.Builders.Insert;
using System.Security.Claims;
using TheSharks.Domain.Entities;

namespace TheSharks.FluentMigration.Seed;

public static class M20220602100743_Seed
{
    private const string AspNetUsers = "AspNetUsers";
    private const string AspNetRoles = "AspNetRoles";
    private const string AspNetRoleClaims = "AspNetRoleClaims";
    private const string AspNetUserRoles = "AspNetUserRoles";

    private const string Galleries = "Galleries";
    private const string NewsItems = "NewsItems";
    private const string Enrollments = "Enrollments";
    private const string MemberEnrollments = "MemberEnrollments";
    private const string GuestEnrollments = "GuestEnrollments";
    private const string Activities = "Activities";

    public static void SeedData(this Migration migration)
    {
        var insert = migration.Insert;
        List<Role> roles = new List<Role>()
        {
            new Role { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcernsDivingCertificate = false, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "Nvt", NormalizedName = "NVT", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "1*", NormalizedName = "1*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "2*", NormalizedName = "2*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "3*", NormalizedName = "3*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "I1*", NormalizedName = "I1*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "I2*", NormalizedName = "I2*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "I3*", NormalizedName = "I3*", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "AI", NormalizedName = "AI", ConcernsDivingCertificate = true, ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = Guid.NewGuid(), Name = "Voorzitter", NormalizedName = "VOORZITTER", ConcernsDivingCertificate = false, ConcurrencyStamp = Guid.NewGuid().ToString()  },
         };

        roles.ForEach(x => insert.IntoTable(AspNetRoles).Row(new { Id = x.Id, Name = x.Name, NormalizedName = x.NormalizedName, ConcernsDivingCertificate = x.ConcernsDivingCertificate, ConcurrencyStamp = x.ConcurrencyStamp }));

        var manageMembers = new Claim("ManageMembers", "CanManageMembers");
        var manageActivities = new Claim("ManageActivities", "CanManageActivities");
        var manageNewsItems = new Claim("ManageNewsItems", "CanManageNewsItems");
        var manageDownloadables = new Claim("ManageDownloadables", "CanManageDownloadables");
        var manageGalleries = new Claim("ManageGalleries", "CanManageGalleries");
        var managePageContent = new Claim("ManagePageContent", "CanManagePageContent");
        var manageStatistics = new Claim("ManageStatistics", "CanManageStatistics");

        addClaimsToRole(roles[0], new List<Claim> { manageMembers, manageActivities, manageNewsItems, manageDownloadables, manageGalleries, managePageContent, manageStatistics }, insert);


        List<Member> members = new List<Member>
        {
            new Member { Id = Guid.NewGuid(), Email = "jos@gmail.com", NormalizedEmail = "JOS@GMAIL.COM", UserName = "Joske", NormalizedUserName = "JOSKE", FirstName = "Jos", LastName = "Vermeulen", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "test@gmail.com", NormalizedEmail = "TEST@GMAIL.COM", UserName = "Thomaske", NormalizedUserName = "THOMASKE", FirstName = "Thomas", LastName = "Sarens", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "fracis@gmail.com", NormalizedEmail = "FRANCIS@GMAIL.COM", UserName = "Fraciske", NormalizedUserName = "FRANCISKE", FirstName = "Fracis", LastName = "Claes", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "angèle@gmail.com", NormalizedEmail = "ANGELE@GMAIL.COM", UserName = "Angèleke", NormalizedUserName = "ANGELEKE", FirstName = "Angèle", LastName = "Roberts", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "piet@gmail.com", NormalizedEmail = "PIET@GMAIL.COM", UserName = "Pieteke", NormalizedUserName = "PIETEKE", FirstName = "Piet", LastName = "Slaegers", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "gerry@gmail.com", NormalizedEmail = "GERRY@GMAIL.COM", UserName = "Gerry123", NormalizedUserName = "GERRY123", FirstName = "Gerry", LastName = "Vanboven", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "robbie@gmail.com", NormalizedEmail = "ROBBIE@GMAIL.COM", UserName = "Robbiee", NormalizedUserName = "ROBBIEE", FirstName = "Robbie", LastName = "Schuurmans", SecurityStamp = Guid.NewGuid().ToString()},
            new Member { Id = Guid.NewGuid(), Email = "albrecht@gmail.com", NormalizedEmail = "ALBRECHT@GMAIL.COM", UserName = "Albrechtde5de", NormalizedUserName = "ALBRECHTDE5DE", FirstName = "Albrecht", LastName = "De Koning", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "anne@gmail.com", NormalizedEmail = "ANNE@GMAIL.COM", UserName = "Anneke", NormalizedUserName = "ANNEKE", FirstName = "Anne", LastName = "Vrancken", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "ben@gmail.com", NormalizedEmail = "BEN@GMAIL.COM", UserName = "Bennie", NormalizedUserName = "BENNIE", FirstName = "Ben", LastName = "Weyts", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "balthazaar@gmail.com", NormalizedEmail = "BALTHAZAAR@GMAIL.COM", UserName = "Ballie", NormalizedUserName = "BALLIE", FirstName = "Balthazar", LastName = "Bomans", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "bruno@gmail.com", NormalizedEmail = "BRUNO@GMAIL.COM", UserName = "Bruno1", NormalizedUserName = "BRUNO", FirstName = "Bruno", LastName = "Vlaanderen", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "chris@gmail.com", NormalizedEmail = "CHRIS@GMAIL.COM", UserName = "Chriske", NormalizedUserName = "CHRISKE", FirstName = "Chris", LastName = "De Ruyter", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "daniel@gmail.com", NormalizedEmail = "DANIEL@GMAIL.COM", UserName = "Daniela", NormalizedUserName = "DANIELA", FirstName = "Daniel", LastName = "Janssens", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "gert@gmail.com", NormalizedEmail = "GERT@GMAIL.COM", UserName = "Gerrie", NormalizedUserName = "GERRIE", FirstName = "Gert", LastName = "Van Den Berg", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "hilde@gmail.com", NormalizedEmail = "HILDE@GMAIL.COM", UserName = "Hilde", NormalizedUserName = "HILDE", FirstName = "Hilde", LastName = "Peeters", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "axel@gmail.com", NormalizedEmail = "AXEL@GMAIL.COM", UserName = "Axel", NormalizedUserName = "AXEL", FirstName = "Axel", LastName = "Wouters", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "lena@gmail.com", NormalizedEmail = "LENA@GMAIL.COM", UserName = "Lena", NormalizedUserName = "LENA", FirstName = "Lena", LastName = "Jacobs", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "jeroen@gmail.com", NormalizedEmail = "JEROEN@GMAIL.COM", UserName = "Jeroen", NormalizedUserName = "JEROEN", FirstName = "Jeroen", LastName = "Celis", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "koen@gmail.com", NormalizedEmail = "KOEN@GMAIL.COM", UserName = "Koen", NormalizedUserName = "KOEN", FirstName = "Koen", LastName = "Van Espen", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "joris.rombauts2@hotmail.com", NormalizedEmail = "JORIS.ROMBAUTS2@HOTMAIL.COM", UserName = "Jeuris", NormalizedUserName = "JEURIS", FirstName = "Joris", LastName = "Rombauts", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "maxim.compeers@gmail.com", NormalizedEmail = "MAXIM.COMPEERS@GMAIL.COM", UserName = "Maxim", NormalizedUserName = "MAXIM", FirstName = "Maxim", LastName = "Compeers", SecurityStamp = Guid.NewGuid().ToString() },
            new Member { Id = Guid.NewGuid(), Email = "johnny.hooyberghs@gmail.com", NormalizedEmail = "JOHNNY.HOOYBERGHS@GMAIL.COM", UserName = "Johnny", NormalizedUserName = "JOHNNY", FirstName = "Johnny", LastName = "Hooyberghs", SecurityStamp = Guid.NewGuid().ToString() },
        };

        members.ForEach(x => insert.IntoTable(AspNetUsers).Row(new { Id = x.Id, Email = x.Email, NormalizedEmail = x.NormalizedEmail, FirstName = x.FirstName, LastName = x.LastName, UserName = x.UserName, NormalizedUserName = x.NormalizedUserName, SecurityStamp = x.SecurityStamp }));

        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[0].Id, UserId = members[0].Id });  // Jos = admin
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[1].Id, UserId = members[0].Id });  // Jos = nvt brevet
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[9].Id, UserId = members[1].Id });  // Thomas = voorzitter
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[4].Id, UserId = members[2].Id });  // Francis = 3* duiker 
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[3].Id });  // Angèle  = 1* duiker 
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[5].Id, UserId = members[4].Id });  // Piet = 1* instructeur
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[7].Id, UserId = members[5].Id });  // Gerry = 3* instructeur 
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[6].Id });  // Robbie = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[7].Id });  // Albrecht = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[8].Id });  // Anne = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[9].Id });  // Ben = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[8].Id, UserId = members[10].Id }); // Balthazar = AI
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[11].Id }); // Bruno = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[12].Id }); // Chris = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[3].Id, UserId = members[13].Id }); // Daniel = 2* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[3].Id, UserId = members[14].Id }); // Gert = 2* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[4].Id, UserId = members[15].Id }); // Hilde = 3* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[4].Id, UserId = members[16].Id }); // Axel = 3* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[17].Id }); // Lena = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[3].Id, UserId = members[18].Id }); // Jeroen = 2* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[2].Id, UserId = members[19].Id }); // Koen = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[0].Id, UserId = members[20].Id }); // Joris = admin
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[1].Id, UserId = members[20].Id }); // Joris = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[0].Id, UserId = members[21].Id }); // Maxim = admin
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[1].Id, UserId = members[21].Id }); // Maxim = 1* duiker
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[0].Id, UserId = members[22].Id }); // Johnny = admin
        insert.IntoTable(AspNetUserRoles).Row(new { RoleId = roles[1].Id, UserId = members[22].Id }); // Johnny = 1* duiker


        insert.IntoTable(Galleries).Row(new { Name = "Zuidbout 2016", CreationDate = new DateTimeOffset(2016, 4, 5, 12, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Opprebais 2017", CreationDate = new DateTimeOffset(2017, 8, 23, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "La Gombe 2018", CreationDate = new DateTimeOffset(2018, 6, 19, 14, 38, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "BBQ Duik 2018", CreationDate = new DateTimeOffset(2018, 7, 12, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Grevelingenweekend 2018", CreationDate = new DateTimeOffset(2018, 5, 13, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Rebreathertraining 2018", CreationDate = new DateTimeOffset(2018, 10, 15, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Clubduik 2018", CreationDate = new DateTimeOffset(2018, 6, 23, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Nieuwjaarsduik 2018", CreationDate = new DateTimeOffset(2017, 12, 3, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Bootjesduik 2017", CreationDate = new DateTimeOffset(2017, 9, 3, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Spaanse duik 2017", CreationDate = new DateTimeOffset(2017, 7, 10, 16, 0, 0, new TimeSpan(1, 0, 0)) });
        insert.IntoTable(Galleries).Row(new { Name = "Malta 2013", CreationDate = new DateTimeOffset(2013, 8, 12, 16, 0, 0, new TimeSpan(1, 0, 0)) });


        insert.IntoTable(NewsItems).Row(new
        {
            Title = "2020+2",
            Content = "<div class=\"entry-content\"><p>Beste sharks en bezoekers,</p>" +
        "<p>Ons feestjaar van  2020 voor ons 50jarig bestaan gaat dit jaar voor de derde maal van start, " +
            "hoera!<br>We denken er immidels over van dit te blijven vieren, zo is het altijd feest en worden we geen dag ouder 😎" +
            "</p>" +
            "<p>Maandag 10/01 zijn er gratis worstenbroden en appelbollen na de training en maken we zo van verloren" +
            " maandag een gewonnen maandag.</p><p>Op 16/01 brengen we een toast uit op het nieuwe jaar, locatie Put van Ekeren." +
            "<br>Het feestcomité voorziet hotdogs en lekkere drankjes, noteren in de agenda en komen is dus de boodschap!</p>" +
            "<p>Alvast een warm, liefdevol en gezond 2022 🤗" +
            "</p> " +
            "<p>De voorzitter</p></div>",
            PublishDate = new DateTimeOffset(2022, 1, 9, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Omleiding",
            Content = "<div class=\"entry-content\">" +
            "<p>Een omleiding komt altijd ongelegen. </p>" +
            "<p>We zijn steeds op weg, een nieuw avontuur of de gewone dagelijkse gang van zaken." +
            "Gericht op de bestemming en het meest directe pad om daar te geraken. " +
            "Soms moeten we door overmacht een omweg inslaan, die soms is tegenwoordig nogal regelmatig, en dat is lastig.</p>" +
            "<p>“You should enjoy the little detours to the fullest. Because that’s where you’ll find the things " +
            "more important than what you want.” Yoshihiro</p><p>Hoe vervelend de omweg ook is, blijf genieten van wat er op je pad " +
            "komt.</p></div>",
            PublishDate = new DateTimeOffset(2021, 11, 28, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "U komt toch ook?",
            Content = "<div class=\"entry-content\">" +
            "<p>Het nieuwe schooljaar gaat binnenkort weer van start en dus ook onze zwembadlessen.Het lijkt erop, " +
            "als ik het nieuws goed heb gevolgd, dat we vanaf 1 september meer vrijheden terugkrijgen. " +
            "Dat is alvast hoopgevend, zeker voor kandidaten die ondertussen er al een hele tijd naar uitkijken om hun " +
            "brevet af te werken. Bonne chance à vous tous, oftewel doet da goed hé!</p> <p>Volgens den Dikke Van Dale " +
            "is een spreekwoord: een uitspraak &nbsp; die een algemene levenservaring of wijze les bevat." +
            "Graag dan ook even de aandacht voor het volgende spreekwoord “<strong>Vele handen maken licht werk</strong>“. " +
            "The Sharks rekent als club op de vrijwillige inzet van haar leden om onze prachtige hobby met veel plezier " +
            "te kunnen uitvoeren. Het bestuur en het monitariaat proberen dit steeds in goede banen te leiden, " +
            "maar sfeer en gezelligheid kunnen we niet maken. Daarvoor rekenen we op de aanwezigheid en het enthousiasme van iedereen 🥳" +
            "</p>" +
            "<p>Wil je dus graag eens een activiteit organiseren voor de club, de club ergens promoten, iets meebrengen " +
            "naar een clubduik, iets anders? Laat dan zeker van je horen 🦈" +
            "</p>" +
            "</div>",
            PublishDate = new DateTimeOffset(2021, 8, 21, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Aprilse Grillen",
            Content = "<div class=\"entry-content\">" +
            "<p>De maand april loopt bijna op een einde en daarmee hopelijk ook haar grillen.Niet alleen het weer durfde " +
            "de laatste tijd wel eens snel omslagen, maar ook de maatregelen om dat beruchte virus te proberen in te perken. " +
            "Hopelijk mist de maand mei haar intrede niet en brengt ze perspectief en een zonnige periode op alle vlakken.</p>" +
            "<p>Dat we elkaars gezelschap al zo lang moeten missen is natuurlijk een pijnlijke vaststelling. Clubduiken of " +
            "activiteiten mogen helaas nog steeds niet georganiseerd worden. Maar een haai vindt altijd zijn weg, ik kan jullie " +
            "dan ook enkel aanmoedigen om met je duikbuddy af te spreken en veilig samen een duikje te gaan doen in de gezonde " +
            "buitenlucht.Gebruik hiervoor gerust ook onze Faceboek groep (of een mailtje/sms he).</p>" +
            "<p>Hou vol, doe ze vol en schol,<br>De voorzitter</p></div>",
            PublishDate = new DateTimeOffset(2021, 4, 23, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Gelukkige Feestdagen",
            Content = "<div class=\"entry-content\">" +
            "<p>21 december, dag van de winter zonnewende.De zon begint haar schijnbare trektocht naar de Kreeftskeerkring en " +
            "onze dagen worden terug langer en langer.</p><p>Met die hoopvolle gedachte wens ik graag iedereen " +
            "gelukkige feestdagen, een goede gezondheid en veel liefde.Dat 2021 mag goedmaken waar het jaar-dat-niet-zal-vernoemd-worden" +
            " tekort schoot.</p> <p>De feestdagen zullen wellicht anders dan anders zijn, maar gelukkig maken we ons eigen geluk." +
            "Hoe dan? Toon Hermans zegt het mooi met een gedichtje:</p> <blockquote> <p style = \"text-align: center;\" > Geluk is " +
            "geen kathedraal,<br>misschien een klein kapelletje.<br> Geen kermis luid en kolossaal,<br> misschien een carrouselletje.</p>" +
            " <p style = \"text-align: center;\" > Geluk is geen zomer van smetteloos blauw,<br>maar nu en dan een zonnetje.<br>" +
            "Geluk dat is geen zeppelin,<br>‘t is hooguit ‘n ballonnetje.</p></blockquote><p>Lieve Sharks, blijf het geluk vinden " +
            "in de kleine dingen. Want wie zich steeds laat verrassen door harlekijnslakje met plezier, " +
            "stuit soms geheel onverwacht op een zeepaardje in het wier.</p><p>Jullie voorzitter,<br>Thomas</p></div>",
            PublishDate = new DateTimeOffset(2020, 12, 21, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Een korte update voor de feestdagen",
            Content = "<div class=\"entry-content\">" +
            "<p>Beste Sharks,</p><p>De feestdagen komen er aan, hoera want het einde van 2020 is daarmee in zicht.</p>" +
            "<p>Onze clubactiviteiten en zwembadtraining liggen helaas weer stil, laat ons zachtjes hopen op een positieve " +
            "evolutie voor december-januari. Aan ons zal het wel niet gelegen hebben want het zwembad was vol lof over " +
            "onze aanpak om alles veilig te laten verlopen(dikke pluim aan iedereen, da emme we goe gedaan).</p>" +
            "<p>Als vervangactiviteit stel ik voor dat iedereen op maandagavond zich thuis gezellig in eigen zetel zet " +
            "met daarbij ne lekkere koude Duvel of een fruitig glaasje rosé of iets anders.Deel dit gerust ook " +
            "op de<a href= \"https://www.facebook.com/groups/38583083025/\"> Sharks Facebook</a> pagina, om ter gezelligst! 😉" +
            "</p>" +
            "<p>Buiten duiken kan en mag nog steeds, maximaal met 4 vaste contacten(zie Nelos info nr 411). " +
            "Georganiseerde clubduiken mogen echter niet, dus zelf initiatief nemen is de boodschap.Zeg nu zelf, " +
            "zo’n lekkere frisse winterduik is de ideale afwisseling voor al die gezellige warmte binnen.&nbsp;</p>" +
            "<p>&nbsp;Geniet nog van het voor november uitzonderlijke goede weer en hou jullie goed, we’ll be back.</p><p>Jullie voorzitter" +
            "</p></div>",
            PublishDate = new DateTimeOffset(2020, 11, 8, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });


        insert.IntoTable(NewsItems).Row(new
        {
            Title = "De tweede golf",
            Content = "<div class=\"entry-content\"><p>Beste Sharks,</p>" +
            "<p>Zoals gevreesd lijken we een tweede(hopelijk kleinere) " +
               "covid-golf tegemoet te gaan.&nbsp; Veiligheid is bij The Sharks een prioriteit en daarom hebben we nu tijdelijk " +
               "alle clubduiken afgelast, zelfs nog voor de aankondiging van nieuwe maatregelen van de veiligheidsraad.</p>" +
               "<p>Zolang het niet verboden wordt staat het iedereen natuurlijk vrij om zelf te gaan duiken.Best wel met 2 " +
               "of zeer beperkte groep (bubbels enzo..) en met de nodige veiligheidsmaatregelen.Voor mensen die willen duiken " +
               "en een buddy zoeken kan je altijd zelf iemand contacteren via onze ledenlijst op de site.Beginnende duikers " +
               "kunnen daarvoor best eens bij hun brevethoofd horen.</p>" +
               "<p>Ons geduld wordt wat op de proef gesteld. 2020 mag stilaan wel gaan stoppen met zo’n streken uit te halen, " +
               "maar ik ben er zeker van dat de toekomstige activiteiten eens te leuker zal maken.Honger is en blijft de beste saus! ?</p>" +
               "<p>Groeten,<br>Thomas</p></div>",
            PublishDate = new DateTimeOffset(2020, 7, 28, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Einde-lockdown",
            Content = "<div class=\"entry-content\"><p>Beste Sharks,</p>" +
            "<p>EINDELIJK! ?</p><p>Hopelijk verkeer je, wanneer je dit leest, " +
            "in goede gezondheid.Het zijn rare en stille tijden geweest, maar er is zoals steeds licht aan het einde van de tunnel." +
            "Nu de grens met Nederland weer open is, kunnen we terug clubduiken organiseren, en wees gerust die komen eraan. " +
            "Hou dus zeker de activiteitenlijst op de website goed in het oog.</p><p>Hoewel we uiteraard zeer blij zijn dat we " +
            "weer mogen samen komen en clubduiken kunnen organiseren, is dit wel nog onder bepaalde voorwaarden. " +
            "COVID-19 is nog niet uitgeroeid en daar moeten we allemaal aandachtig voor blijven.De volledige richtlijnen " +
            "zullen worden doorgestuurd en zullen ook op de pagina met clubactivteiten worden gezet. Een belangrijke maatregel " +
            "die ik hier wel al wil vermelden, is dat het online inschrijven voor de clubduik verplicht is en dat dit ten " +
            "laatste 2 dagen op voorhand moet gebeuren.De duikverantwoordelijke krijgt zo tijd om voor iedereen een buddy te vinden. " +
            "Ik richt mij dan in het bijzonder ook aan onze beginnende duikers die graag hun doopduiken willen doen: niet twijfelen " +
            "en gewoon inschrijven?</p><p>De training en samenkomst in het zwembad op maandagavond&nbsp;is momenteel voor " +
            "velen een groot gemis.Niet alle haaien zijn eenzame jagers zo blijkt, ik mis iedereen alvast en denk/hoop dat " +
            "ik niet alleen ben. Het lijkt erop dat de zwembaden mogelijks binnenkort terug opengaan voor zwemclubs, " +
            "maar hoe het juist zit voor onze zwembadtraining is nog niet zo duidelijk. Aangezien we in juli en augustus " +
            "altijd onze zomerpauze hebben, zullen we daarom als het mag (laat ons hopen!) in september terug van start gaan.</p>" +
            "<p>Nog even op de tanden bijten, gezond blijven, de richtlijnen opvolgen en vooral DUIKEN DUIKEN DUIKEN</p>" +
            "<p>Groeten en tot aan de waterkant,<br>Thomas</p></div>",
            PublishDate = new DateTimeOffset(2020, 6, 22, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[1].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "Coronacrisis",
            Content = "<div class=\"entry-content\"><p><em>Beste bezoeker, " +
            "sympathisant</em></p><p><em>Tijdens deze ongetwijfeld moeilijke " +
            "tijden waarin we mekaar niet kunnen ontmoeten, laat staan duiken of trainen, zijn ook wij genoodzaakt onze activiteiten " +
            "tijdelijk te staken.</em></p><p><em>Uiteraard blijven velen van ons ‘actief’ op allerhande sociale media en dat is goed," +
            " laat ons vooral fijne herinneringen delen en voorzichtig plannen smeden voor na deze ‘onderbreking’!</em></p>" +
            "<p><em>Het Bestuur van The Sharks blijft alles op de voet volgen en zal mee de beslissingen van het Duikonderricht " +
            "van onze federatie Nelos helpen uitdragen. </em></p><p><em>We hopen dan ook dat ieder zijn of haar " +
            "verantwoordelijkheid neemt en de richtlijnen die we recent van de overheid en van Nelos mochten ontvangen " +
            "strikt opvolgt.</em></p><p><em>Aan onze leden maar ook aan alle bezoekers van onze site, " +
            "een goede gezondheid gewenst, veel sterkte en vooral volharding. </em></p><p><em>Met Sportieve groet,</em></p><p><em>Het Bestuur</em><br>" +
            "<img src = \"https://www.thesharks.be/wp-content/uploads/2020/03/Logo_50_Jaar.gif\" alt= \"50 jaar The Sharks\"></p></div>"
            ,
            PublishDate = new DateTimeOffset(2020, 3, 24, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[0].Id
        });

        insert.IntoTable(NewsItems).Row(new
        {
            Title = "50 jaar the sharks",
            Content = "<div class=\"entry-content\"><p>Beste bezoeker,<br>" +
             "<img src = \"https://www.thesharks.be/wp-content/uploads/2020/02/Logo06_Anim_V2.gif\" alt=\"50 jaar The Sharks\"><br>" +
             "50 jaar al streven gedreven monitoren naar een hoog duikschoolniveau.Deze tocht naar perfectie wordt dit jaar " +
             "beklonken met een koninklijke viering.Met niet geringe trots zal er op tal van activiteiten hulde gebracht " +
             "worden aan de iconen van onze Duikschool.<br>De apotheose van dit feestjaar is voor het Grote Sharksfeest " +
             "op 24 oktober 2020.<br>Wens je op de hoogte te zijn van al onze plannen of beter nog, als je erbij wil zijn, " +
             "is het voldoende om lid te worden van onze club.Om iedereen de kans te geven zich op het geknipte niveau te " +
             "engageren serveren we een waaier van mogelijkheden.<br>Ben je geïnteresseerd, stuur gewoon een mailtje naar onze " +
             "secretaris op secretaris @thesharks.be , reactie verzekerd.<br>Het Bestuur van Duikschool The Sharks vzw</p></div>"
             ,
            PublishDate = new DateTimeOffset(2020, 2, 27, 12, 0, 0, new TimeSpan(1, 0, 0)),
            AuthorId = members[0].Id
        });

        List<DiveActivity> dActivities = new List<DiveActivity>()
        {
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Strijenham", Date = new DateTimeOffset(2022, 5, 29, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Strijenham", NecessarySubscription  = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 9, 0, 0, new TimeSpan(1, 0, 0)), Tide = "LW 9:55", Responsible  = members[4] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Wemeldinge parking", Date = new DateTimeOffset(2022, 6, 5, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Wemeldinge parking", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 12, 40, 0, new TimeSpan(1, 0, 0)), Tide = "LW 13:55", Responsible = members[4] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Wemeldinge parking", Date = new DateTimeOffset(2022, 6, 12, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Wemeldinge parking", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 13, 45, 0, new TimeSpan(1, 0, 0)), Tide = "HW 14:45", Responsible = members[5] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Put van Ekeren", Date = new DateTimeOffset(2022, 6, 19, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Put van Ekeren", MemberInfo = "meer informatie volgt", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 10, 30, 0, new TimeSpan(1, 0, 0)), Responsible = members[1] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Sint Annaland", Date = new DateTimeOffset(2022, 6, 26, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Sint Annaland", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 13, 50, 0, new TimeSpan(1, 0, 0)), Tide = "HW 15:05", Responsible = members[4] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Wemeldinge parking", Date = new DateTimeOffset(2022, 7, 10, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Wemeldinge parking", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 11, 45, 0, new TimeSpan(1, 0, 0)), Tide = "HW 13:10", Responsible = members[4] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Vuilbak", Date = new DateTimeOffset(2022, 7, 17, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Vuilbak", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 13, 10, 0, new TimeSpan(1, 0, 0)), Tide = "HW 13:10", Responsible = members[4] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Tetjes", Date = new DateTimeOffset(2022, 8, 14, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Tetjes", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 12, 5, 0, new TimeSpan(1, 0, 0)), Tide = "LW 12:05", Responsible = members[5] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Vuilbak", Date = new DateTimeOffset(2021, 8, 21, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Vuilbak", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 10, 30, 0, new TimeSpan(1, 0, 0)), Tide = "HW 11:25", Responsible = members[5] },
            new DiveActivity { Id = Guid.NewGuid(), ActivityType = "dive", Name = "Duik - Putti's Place", Date = new DateTimeOffset(2022, 8, 28, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Putti's Place", NecessarySubscription = true, BriefingTime = new DateTimeOffset(2022, 5, 29, 9, 0, 0, new TimeSpan(1, 0, 0)), Tide = "LW 11:10", Responsible = members[8] },
        };

        List<EventActivity> eActivities = new List<EventActivity>()
        {
           new EventActivity{ Id = Guid.NewGuid(), ActivityType = "event", Name = "Evenement - Feest 50 jaar The Sharks", Date = new DateTimeOffset(2022, 10, 22, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Feest 50 jaar The Sharks", MemberInfo = "meer informatie volgt", NecessarySubscription = false, Responsible = members[1] }
        };

        List<BoardMeetingActivity> bActivities = new List<BoardMeetingActivity>()
        {
            new BoardMeetingActivity { Id = Guid.NewGuid(), ActivityType = "boardmeeting", Name = "Bestuursvergadering - Vergaderzaal the sharks", Date = new DateTimeOffset(2022, 7, 24, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Vergaderzaal the sharks", NecessarySubscription = false, Responsible = members[1] },
            new BoardMeetingActivity  { Id = Guid.NewGuid(), ActivityType = "boardmeeting", Name = "Bestuursvergadering - Vergaderzaal the sharks", Date = new DateTimeOffset(2022, 7, 31, 0, 0, 0, new TimeSpan(1, 0, 0)), Location = "Vergaderzaal the sharks", NecessarySubscription = false, Responsible = members[1] }
        };

        dActivities.ForEach(x => insert.IntoTable(Activities).Row(new { Id = x.Id, ActivityType = x.ActivityType, Name = x.Name, Date = x.Date, Location = x.Location, NecessarySubscription = x.NecessarySubscription, Info = x.Info, MemberInfo = x.MemberInfo, BriefingTime = x.BriefingTime, Tide = x.Tide, ResponsibleId = x.Responsible.Id }));
        bActivities.ForEach(x => insert.IntoTable(Activities).Row(new { Id = x.Id, ActivityType = x.ActivityType, Name = x.Name, Date = x.Date, Location = x.Location, NecessarySubscription = x.NecessarySubscription, Info = x.Info, MemberInfo = x.MemberInfo, ResponsibleId = x.Responsible.Id }));
        eActivities.ForEach(x => insert.IntoTable(Activities).Row(new { Id = x.Id, ActivityType = x.ActivityType, Name = x.Name, Date = x.Date, Location = x.Location, NecessarySubscription = x.NecessarySubscription, Info = x.Info, MemberInfo = x.MemberInfo, ResponsibleId = x.Responsible.Id }));


        // D A1
        addMemberEnrollement(registree: members[10], registrator: members[10], Guid.NewGuid(), true, dActivities[0], insert);
        addMemberEnrollement(registree: members[4], registrator: members[4], Guid.NewGuid(), true, dActivities[0], insert);

        // D A2
        addMemberEnrollement(registree: members[4], registrator: members[4], Guid.NewGuid(), true, dActivities[1], insert);
        addMemberEnrollement(registree: members[1], registrator: members[1], Guid.NewGuid(), true, dActivities[1], insert);
        addMemberEnrollement(registree: members[7], registrator: members[7], Guid.NewGuid(), true, dActivities[1], insert);
        addMemberEnrollement(registree: members[15], registrator: members[7], Guid.NewGuid(), true, dActivities[1], insert);

        // D A3
        addMemberEnrollement(registree: members[5], registrator: members[5], Guid.NewGuid(), true, dActivities[2], insert);
        addGuestEnrollment(registree: "Stef Jacobs", registrator: members[5], Guid.NewGuid(), "nvt", dActivities[2], insert);

        // D A4
        addMemberEnrollement(registree: members[1], registrator: members[1], Guid.NewGuid(), true, dActivities[3], insert);
        addMemberEnrollement(registree: members[18], registrator: members[1], Guid.NewGuid(), true, dActivities[3], insert);
        addMemberEnrollement(registree: members[7], registrator: members[7], Guid.NewGuid(), true, dActivities[3], insert);
        addMemberEnrollement(registree: members[12], registrator: members[12], Guid.NewGuid(), true, dActivities[3], insert, "Graag openwaterproef B4");
        addMemberEnrollement(registree: members[9], registrator: members[12], Guid.NewGuid(), false, dActivities[3], insert);
        addMemberEnrollement(registree: members[6], registrator: members[6], Guid.NewGuid(), true, dActivities[3], insert, "300m Palm proef B1");

        // D A5
        addMemberEnrollement(registree: members[4], registrator: members[4], Guid.NewGuid(), true, dActivities[4], insert);

        // D A6
        addMemberEnrollement(registree: members[4], registrator: members[4], Guid.NewGuid(), true, dActivities[5], insert);

        // D A7
        addMemberEnrollement(registree: members[4], registrator: members[4], Guid.NewGuid(), true, dActivities[6], insert);

        // D A8
        addMemberEnrollement(registree: members[5], registrator: members[5], Guid.NewGuid(), true, dActivities[7], insert);

        // D A9
        addMemberEnrollement(registree: members[5], registrator: members[5], Guid.NewGuid(), true, dActivities[8], insert);

        // D A10
        addMemberEnrollement(registree: members[8], registrator: members[8], Guid.NewGuid(), true, dActivities[9], insert);

        // E A1
        addMemberEnrollement(registree: members[1], registrator: members[1], Guid.NewGuid(), false, eActivities[0], insert);

        // B A1
        addMemberEnrollement(registree: members[1], registrator: members[1], Guid.NewGuid(), false, bActivities[0], insert);
        addMemberEnrollement(registree: members[20], registrator: members[20], Guid.NewGuid(), false, bActivities[0], insert);
        addMemberEnrollement(registree: members[21], registrator: members[21], Guid.NewGuid(), false, bActivities[0], insert);

        // B A2
        addMemberEnrollement(registree: members[20], registrator: members[20], Guid.NewGuid(), false, bActivities[1], insert);
    }

    private static void addClaimsToRole(Role role, List<Claim> claims, IInsertExpressionRoot insert)
    {
        claims.ForEach(x => insert.IntoTable(AspNetRoleClaims).Row(new { ClaimType = x.Type, ClaimValue = x.Value, RoleId = role.Id }));
    }

    private static void addMemberEnrollement(Member registree, Member registrator, Guid initialEnrollmentId, bool asDiver, Activity activity, IInsertExpressionRoot insert, string? remark = null)
    {
        insert.IntoTable(Enrollments).Row(new { Id = initialEnrollmentId, RegistratorId = registrator.Id, ActivityId = activity.Id, Remark = remark });
        insert.IntoTable(MemberEnrollments).Row(new { Id = initialEnrollmentId, AsDiver = asDiver, RegistreeId = registree.Id });
    }

    private static void addGuestEnrollment(string registree, Member registrator, Guid initialEnrollmentId, string diveLevel, Activity activity, IInsertExpressionRoot insert, string? remark = null)
    {
        insert.IntoTable(Enrollments).Row(new { Id = initialEnrollmentId, RegistratorId = registrator.Id, ActivityId = activity.Id, Remark = remark });
        insert.IntoTable(GuestEnrollments).Row(new { Id = initialEnrollmentId, Registree = registree, GuestType = "Familie", DiveLevel = diveLevel });
    }
}