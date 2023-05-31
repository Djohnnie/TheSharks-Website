namespace TheSharks.Contracts;

public static class StringConstants
{
    public const string NEW_ACCOUNT_EMAIL_SUBJECT =
        "The Sharks: Ledentoegang tot www.thesharks.be";
    public const string NEW_ACCOUNT_EMAIL_BODY =
        "Beste {0},<br><br><br>Welkom bij The Sharks!<br>We hebben een toegang voor je aangemaakt op www.thesharks.be via dit email adres.<br><br>Je gebruikersnaam is <b>{1}</b> en je wachtwoord kan je instellen via onderstaande link:<br><a href=\"{2}\">Wachtwoord kiezen</a><br><br><br>Met vriendelijke groeten,<br>The Sharks";

    public const string PASSWORD_RESET_REQUEST_EMAIL_SUBJECT = 
        "The Sharks: Wachtwoord vergeten";
    public const string PASSWORD_RESET_REQUEST_EMAIL_BODY =
        "Beste {0},<br><br><br>Je hebt een wachtwoordherstel aangevraagd via de website van The Sharks.<br>Klik op de link hieronder om een nieuw wachtwoord te kiezen:<br><a href=\"{1}\">Wachtwoord kiezen</a><br><br>Heb je geen wachtwoordherstel aangevraagd, dan heeft iemand anders dat waarschijnlijk in jouw naam gedaan, en mag je deze email gewoon negeren.<br>Je huidige wachtwoord blijft dan ongewijzigd en veilig opgeslagen.<br><br>Niet vergeten! Om aan te melden op de website gebruik je je gebruikersnaam: <b>{2}</b>.<br><br><br>Met vriendelijke groeten,<br>The Sharks";
    
    public const string PASSWORD_RESET_CONFIRM_EMAIL_SUBJECT 
        = "The Sharks: Wachtwoord hersteld";
    public const string PASSWORD_RESET_CONFIRM_EMAIL_BODY =
        "Beste {0},<br><br><br>Je hebt je wachtwoord hersteld en je kan nu met je nieuwe wachtwoord aanmelden op de website.<br><br>Niet vergeten! Om aan te melden op de website gebruik je je gebruikersnaam: <b>{1}</b>.<br><br><br>Met vriendelijke groeten,<br>The Sharks";
}