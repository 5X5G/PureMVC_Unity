using System.Collections;

public class CharacterInfo{

    public int Level { get; set; }
    public int Hp { set; get; }

    public CharacterInfo() { }

    public CharacterInfo(int level, int hp)
    {
        Level = level;
        Hp = hp;
    }
}
