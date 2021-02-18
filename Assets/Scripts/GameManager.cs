using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject Card;
    public GameObject Menu;
    public float suspicionLvL = 0.1f;
    public float treacheryLvL = 0.1f;
    public EventScriptable eventScrip;
    public NounScriptable items;
    public NounScriptable places;
    public NounScriptable names;
    public MessagesHelper current;
    public AvatarHelper currentChar;
    public CharacterScriptable charScrip;
    public GameObject MapMenu;
    public GameObject CardMenu;
    public GameObject BossPanel;
    public GameObject DeathMenu;
    public float timeMax = 30f;
    public int landsTaken = 0;
    public int kingsSlain = 0;
    public float timePassed = 0;
    public List<Sprite> backgroundList = new List<Sprite>();
    public bool isNight = false;
    // parametrykarty
    public Image avatar;
    public Text lore;
    public Text characterName;
    public Image background;
    public Image treachery;
    public Image suspision;
    public GameObject currentLand;
    public Color LandColor;
    public bool turn=false;
    public bool neverused = true;
    public Image avaBackground;
    [Header("EndMenu")]
    public Text landsAmount;
    public Text kingsKilled;
    public Text Tooltip;
    [Header("klepsydra")]
    public Image sand;
    [Header("Boss")]
    public Image bossAvatar;
    public Text bossName;
    public Text bossKillText;
    public Image bossBackground;
    public EventScriptable bossScenarios;
    public MessagesHelper currentBoss;
    public CharacterScriptable BossScript;
    [Header("PlayerHouse")]
    public string dynastyName;
    public float r = 0, g = 0, b = 0;
    public Color dynastyColor;
    public GameObject FamilyScreen;
    public InputField inp;
    public Slider sR,sG,sB;
    public Image ColorCheck;

    public void changeColor()
    {
        r = sR.value;
        g = sG.value;
        b = sB.value;
        dynastyColor = new Color(r, g, b);
        ColorCheck.color = dynastyColor;
    }
    public void AcceptDynasty()
    {
        dynastyName = inp.text.ToString();
        GoToMap();
        FamilyScreen.SetActive(false);
    }
    // Use this for initialization
    private void Awake()
    {
        Instance = this;
    }
    void Start () {
        current = new MessagesHelper();
        currentChar = new AvatarHelper();
       // StartGame();
	}
	
	// Update is called once per frame
	void Update () {
        if(landsTaken >=10)
        {
            DeathMenu.SetActive(true);
            landsAmount.text = "Przejęte landy: " + landsTaken;
            kingsKilled.text = "Królowie zabici: " + kingsSlain;
            Tooltip.text = "Wygrana!";

        }
		if(suspicionLvL >= 100)
        {
            //endgame
            DeathMenu.SetActive(true);
            landsAmount.text = "Przejęte landy: " + landsTaken;
            kingsKilled.text = "Królowie zabici: " + kingsSlain;
            Tooltip.text = "Porażka...";
            treacheryLvL = 0.1f;
            suspicionLvL = 0.1f;
           // treachery.rectTransform.sizeDelta = new Vector2(100, 0.1f);
            //suspision.rectTransform.sizeDelta = new Vector2(100, 0.1f);
            timePassed = 0;
        }
        if(treacheryLvL >= 100)
        {
            //wingame;
            BossPanel.SetActive(true);
            SpawnKingBoss();
           // MapMenu.SetActive(true);
            CardMenu.SetActive(false);
            treacheryLvL = 0.1f;
            suspicionLvL = 0.1f;
            timePassed = 0;
            landsTaken++;
            var colors = currentLand.GetComponent<Button>().colors;
            colors.disabledColor = dynastyColor;
            currentLand.GetComponent<Button>().colors = colors;
            currentLand.GetComponentInChildren<Text>().text = dynastyName;
           // treachery.rectTransform.sizeDelta = new Vector2(100, 0.1f);
           // suspision.rectTransform.sizeDelta = new Vector2(100, 0.1f);
        }
        if(CardMenu.activeSelf)
        {
            timePassed += Time.deltaTime;
            sand.rectTransform.sizeDelta = new Vector2(100, 250 - ((timePassed) / (timeMax / Mathf.Max((landsTaken + suspicionLvL) * 0.027f, 1))) * 250);
            if (timePassed >= (timeMax / Mathf.Max((landsTaken + suspicionLvL) * 0.027f, 1f)))
            {
                timePassed = 0;
                DeathMenu.SetActive(true);
                landsAmount.text = "Przejęte landy: " + landsTaken;
                kingsKilled.text = "Królowie zabici: " + kingsSlain;
                Tooltip.text = "Porażka...";
            }
        }
        
        if(turn)
        {
            if(Card.transform.rotation.y <= 0.7f && neverused)
            {
                Card.transform.Rotate(Vector3.up * (Time.deltaTime * 180));
                Debug.Log(Card.transform.rotation.y);
            }
            if(Card.transform.rotation.y>=0.7f)
            {
                if(neverused)
                {
                    LoadNewCard();
                    neverused = false;
                }
                
            }
            if (Card.transform.rotation.y >= 0f && neverused==false)
            {
                Card.transform.Rotate(Vector3.down * (Time.deltaTime * 180));
                Debug.Log(Card.transform.rotation.y);
            }
        }
    }
    public void SpawnKingBoss()
    {
        currentBoss = bossScenarios.list[Random.Range(0, bossScenarios.list.Count)];
        currentChar = BossScript.avaList[Random.Range(0, BossScript.avaList.Count)];
        string con = currentBoss.eventMsg.Replace("%name%", names.nounList[Random.Range(0, names.nounList.Count)]).Replace("%item%", items.nounList[Random.Range(0, items.nounList.Count)]).Replace("%place%", places.nounList[Random.Range(0, places.nounList.Count)]);
        con = con.Replace("%test%", names.nounList[Random.Range(0, names.nounList.Count)]);
        bossKillText.text = con;
        bossName.text = "Król z dynastii " + currentLand.name.ToString();
        bossAvatar.sprite = currentChar.avatarImg;
        bossBackground.color = LandColor;
    }
    public void StartGame()
    {
        MapMenu.SetActive(false);
        current = eventScrip.list[Random.Range(0, eventScrip.list.Count)];
        Debug.Log(charScrip.avaList[Random.Range(0, charScrip.avaList.Count)]);
        currentChar = charScrip.avaList[Random.Range(0, charScrip.avaList.Count-1)];
        CardMenu.SetActive(true);
        string con = current.eventMsg.Replace("%name%", names.nounList[Random.Range(0, names.nounList.Count)]).Replace("%item%", items.nounList[Random.Range(0, items.nounList.Count)]).Replace("%place%", places.nounList[Random.Range(0, places.nounList.Count)]);
        con = con.Replace("%test%", names.nounList[Random.Range(0, names.nounList.Count)]);
        lore.text = con;
        avatar.sprite = currentChar.avatarImg;
        characterName.text = currentChar.charName;
        avaBackground.color = LandColor;
    }
  
    public void Accept()
    {
        treacheryLvL += current.A_killingProgress;
        suspicionLvL += current.A_suspisionProgress;
        turn = true;
        neverused = true;
        //LoadNewCard();
    }
    public void Decline()
    {
        treacheryLvL -= current.D_killingProgress;
        suspicionLvL -= current.D_suspisionProgress;
        //LoadNewCard();
        turn = true;
        neverused = true;
    }
    public void LoadNewCard()
    {
        if (treacheryLvL <= 0.1f)
        {
            treacheryLvL = 0.1f;
        }
        if(suspicionLvL <= 0.1f)
        {
            suspicionLvL = 0.1f;
        }
        treachery.rectTransform.sizeDelta = new Vector2(100,(195 * treacheryLvL) / 100);
        suspision.rectTransform.sizeDelta = new Vector2(100, (105 * suspicionLvL) / 100);
        sand.rectTransform.sizeDelta = new Vector2(100, 250);
        ChangeDaytime();
        timePassed = 0;
        currentChar = charScrip.avaList[Random.Range(0, charScrip.avaList.Count)];
        current = eventScrip.list[Random.Range(0, eventScrip.list.Count)];
        avatar.sprite = currentChar.avatarImg;
        characterName.text = currentChar.charName;
        string con = current.eventMsg.Replace("%name%", names.nounList[Random.Range(0, names.nounList.Count)]).Replace("%item%", items.nounList[Random.Range(0, items.nounList.Count)]).Replace("%place%", places.nounList[Random.Range(0, places.nounList.Count)]);
        lore.text = con;
        avaBackground.color = LandColor;
    }
    public void ChangeDaytime()
    {
        isNight = !isNight;
        if(isNight)
        {
            background.sprite = backgroundList[1];
        }
        else
        {
            background.sprite = backgroundList[0];
        }
    }
    public void LandClick()
    {
       // StartGame();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void GoToMap()
    {
        MapMenu.SetActive(true);
        Menu.SetActive(false);
    }
    public void CreateFamily()
    {
        FamilyScreen.SetActive(true);
    }
}
