using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUi : MonoBehaviour
{
    public Sprite[] title;
    public Sprite[] lobby;
    public Sprite[] upgrade;
    public Sprite[] artifact;
    public Sprite[] inGame;
    public Sprite[] levelUp;
    public Sprite[] statInventory;
    public Sprite[] attribute;
    public Sprite[] gameResult;
    public Sprite[] setting;

    private string[] titleTexts;
    private string[] lobbyTexts;
    private string[] upgradeTexts;
    private string[] artifactTexts;
    private string[] inGameTexts;
    private string[] levelUpTexts;
    private string[] statInventoryTexts;
    private string[] attributeTexts;
    private string[] gameResultTexts;
    private string[] settingTexts;

    public Sprite[] imageSet;
    public string[] descriptions;
    public GameObject window;
    public Text description;
    public int imageIndex = 0;

    public GameObject[] lobbyChildrens;
    public GameObject[] inGameChildrens;

    public Button[] buttons;

    public void Start()
    {
        SetText();
        Select(0);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => Select(index));
        }
    }

    public void Select(int i)
    {
        imageSet = null;
        imageIndex = 0;

        if (i != 1 && i != 2 && i != 3)
        {
            for (int j = 0; j < lobbyChildrens.Length; j++)
            {
                lobbyChildrens[j].SetActive(false);
            }
        }

        if (i != 4 && i != 5 && i != 6 && i != 7 && i != 8)
        {
            for (int j = 0; j < inGameChildrens.Length; j++)
            {
                inGameChildrens[j].SetActive(false);
            }
        }

        switch (i)
        {
            case 0:
                imageSet = title;
                descriptions = titleTexts;
                break;
            case 1:
                imageSet = lobby;
                descriptions = lobbyTexts;
                for (int j = 0; j < lobbyChildrens.Length; j++)
                { lobbyChildrens[j].SetActive(true); }
                break;
            case 2:
                imageSet = upgrade;
                descriptions = upgradeTexts;
                break;
            case 3:
                imageSet = artifact;
                descriptions = artifactTexts;
                break;
            case 4:
                imageSet = inGame;
                descriptions = inGameTexts;
                for (int j = 0; j < inGameChildrens.Length; j++)
                { inGameChildrens[j].SetActive(true); }
                break;
            case 5:
                imageSet = levelUp;
                descriptions = levelUpTexts;
                break;
            case 6:
                imageSet = statInventory;
                descriptions = statInventoryTexts;
                break;
            case 7:
                imageSet = attribute;
                descriptions = attributeTexts;
                break;
            case 8:
                imageSet = gameResult;
                descriptions = gameResultTexts;
                break;
            case 9:
                imageSet = setting;
                descriptions = settingTexts;
                break;
            default:
                break;
        }

        Image image = window.GetComponent<Image>();
        image.sprite = imageSet[0];
        description.text = descriptions[0];

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }


    public void ImageChange(bool isRight)
    {
        Image image = window.GetComponent<Image>();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);

        if (imageSet.Length > 1)
        {
            if (isRight)
            {
                imageIndex++;
                if (imageIndex == imageSet.Length)
                {
                    imageIndex = 0;
                }
            }
            else if (!isRight)
            {
                imageIndex--;
                if (imageIndex < 0)
                {
                    imageIndex = imageSet.Length - 1;
                }
            }
            image.sprite = imageSet[imageIndex];
            description.text = descriptions[imageIndex];
        }
    }

    public void TutorialOut()
    {
        SceneManager.LoadScene("Lobby");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void SetText()
    {
        titleTexts = new string[4];
        lobbyTexts = new string[1];
        upgradeTexts = new string[6];
        artifactTexts = new string[3];
        inGameTexts = new string[10];
        levelUpTexts = new string[2];
        statInventoryTexts = new string[3];
        attributeTexts = new string[2];
        gameResultTexts = new string[1];
        settingTexts = new string[1];

        titleTexts[0] = "비어있는 슬롯을 선택하여 세이브 파일을 저장할 곳을 정합니다";
        titleTexts[1] = "플레이어의 닉네임을 지정한 후 확인 버튼을 눌러 로비로 넘어갑니다";
        titleTexts[2] = "세이브가 생성된 후 플레이어의 닉네임과 최고 기록을 확인할 수 있습니다";
        titleTexts[3] = "묘비 아이콘을 눌러 플레이어의 세이브를 삭제할 수 있습니다";

        lobbyTexts[0] = "로비 화면에선 게임 시작, 업그레이드, 아티팩트 도감의 기능을 이용할 수 있으며 다시 타이틀로 돌아갈 수도 있습니다";

        upgradeTexts[0] = "업그레이드에선 코인을 사용해 플레이어의 능력치를 올릴 수 있습니다";
        upgradeTexts[1] = "플레이어의 능력치가 이곳에 표시됩니다";
        upgradeTexts[2] = "소지한 코인이 이곳에 표시됩니다";
        upgradeTexts[3] = "각 능력치에 해당되는 아이콘을 눌러 강화를 할 수 있습니다";
        upgradeTexts[4] = "강화에 필요한 코인은 이곳에 표시됩니다";
        upgradeTexts[5] = "강화 포인트가 다 찼을 경우 강화가 되지 않습니다.";

        artifactTexts[0] = "아티팩트 도감에서는 지금까지 획득했던 아티팩트의 정보를 알 수 있습니다";
        artifactTexts[1] = "획득한 아티팩트의 경우 불이 들어오게 됩니다";
        artifactTexts[2] = "아티팩트의 도감 번호와 이름 등급과 속성 및 능력치에 대한 정보를 알 수 있습니다";

        inGameTexts[0] = "게임 시작 시 상단의 타이머에 10분의 시간이 주어지며 이 주어진 시간 동안 최대한 많은 몬스터를 잡으며 생존하는 것이 게임의 목표입니다";
        inGameTexts[1] = "플레이어는 조이스틱으로 움직일 수 있으며 설정 창에서 조이스틱의 설정을 바꿀 수 있습니다";
        inGameTexts[2] = "플레이어에게 몬스터가 닿을 시 체력이 감소하며 플레이어 머리 위의 체력 바를 통해 확인이 가능합니다";
        inGameTexts[3] = "공격은 공격 범위에 몬스터가 들어오게 되면 자동으로 공격하게 됩니다";
        inGameTexts[4] = "몬스터는 플레이어를 향해 이동하면 시간이 지날수록 강력한 몬스터가 등장하게 됩니다";
        inGameTexts[5] = "몬스터를 잡을 시 아이템이 생성되며 이 아이템은 플레이어와 접촉 시 획득할 수 있습니다";
        inGameTexts[6] = "경험치 코인은 종류별로 다른 경험치량을 가지며 플레이어의 획득 범위 및 경험치 배율 능력치에 영향을 받습니다";
        inGameTexts[7] = "박스는 아티팩트를 고를 수 있게 해줍니다";
        inGameTexts[8] = "회복 팩은 플레이어 최대 체력 20%를 회복시켜 줍니다";
        inGameTexts[9] = "자석은 모든 코인을 플레이어에게 끌어옵니다";

        levelUpTexts[0] = "경험치가 가득 찼을 시 레벨업을 합니다";
        levelUpTexts[1] = "3가지 중 하나의 아티팩트를 선택할 수 있으며, 매 레벨업마다 한번의 리셋 기회가 주어집니다";

        statInventoryTexts[0] = "박스 모양의 아이콘을 눌러 상태창을 열 수 있습니다.";
        statInventoryTexts[1] = "상태창에서는 플레이어의 능력치 및 지금까지 획득한 아티팩트에 대해 알 수 있습니다";
        statInventoryTexts[2] = "아티팩트를 눌러 아티팩트의 정보를 열람할 수 있습니다";

        attributeTexts[0] = "특별한 아티팩트를 획득 시 속성 포인트가 상승하게 되며, 속성에 대한 정보는 각 속성을 눌러 확인 가능합니다";
        attributeTexts[1] = "각 속성의 자세한 설명에 대해 확인이 가능하며, 속성 포인트에 따라 속성이 강화됩니다";

        gameResultTexts[0] = "주어진 시간이 종료되거나 플레이어의 체력이 0이 되었을 시 게임 결과 창이 나오게 되며 게임 성적에 관해 확인이 가능합니다. 로비 버튼을 눌러 로비로 돌아갈 수 있습니다.";

        settingTexts[0] = "설정 창에서는 볼륨 및 조이스틱에 대한 설정이 가능하며, 게임 종료 및 게임 포기가 가능합니다";
    }
}
