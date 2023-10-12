using System.Xml.Serialization;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Community jbua = new("Jackbox Ukraine", "TJPP3 (excl TMP), TJPP4 (excl F3), " +
                                                             "TJSP, Drawful 2");
Community tusa = new("TUSA", "TJPP3 (FakinIt), TJPP5 (ZD), TJPP7 (TP), " +
                           "TJPP8 (DA, JJ, WD), TJPP9 (RMG, JT, QX), UYW");
Translator[] ukrainianJackbox = new Translator[]
{
    new(jbua, "Vladyslav", 28, "easy.sugar"),
    new(jbua, "Artem", 18, "artemdurnyk"),
    new(jbua, "Iryna", 22, "Irinniada"),
    new(tusa, "Andrii", 22, "Valerich"),
    new(tusa, "Davyd", 18, "wideprism"),
    new(tusa, "Ivan", 16, "DragonFest"),
    new(tusa, "Mykhailo", 17, "MishaRey")

};
XmlSerializer formatter = new(typeof(Translator[]));
using (FileStream fs = new("person.xml", FileMode.Create))
{
    formatter.Serialize(fs, ukrainianJackbox);
    Console.WriteLine("The object has been successfully serialized.");
}

string prevComm = "";
using (FileStream fs = new("person.xml", FileMode.Open))
{
    Translator[]? translators = formatter.Deserialize(fs) as Translator[];
    if (translators != null)
    {
        foreach (Translator person in translators)
        {
            if (prevComm != person.CommunityName)
            {
                prevComm = person.CommunityName;
                Console.WriteLine(prevComm);
            }
            Console.WriteLine($"Name: {person.Name}\tAge: {person.Age}\t" +
                                            $"Nickname: {person.Nickname}");
        }
    }
}
public class Community
{
    [XmlAttribute]
    public string Name { get; set; } = "undefined";
    public string Games { get; set; } = "None";

    public Community() { }
    public Community(string name, string games)
    {
        Name = name;
        Games = games;
    }
}
public class Translator
{
    public string CommunityName { get; set; } = "None";
    [XmlElement]
    public string Name { get; set; } = "undefined";
    public string Nickname {  get; set; } = "None";
    [XmlElementAttribute(IsNullable = false)]
    public int Age { get; set; } = -1;
    
    
    public Translator() { }
    public Translator(Community community, string name, int age, string nickname)
    {
        CommunityName = community.Name;
        Name = name;
        Age = age;
        Nickname = nickname;
    }
}