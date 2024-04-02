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

        titleTexts[0] = "����ִ� ������ �����Ͽ� ���̺� ������ ������ ���� ���մϴ�";
        titleTexts[1] = "�÷��̾��� �г����� ������ �� Ȯ�� ��ư�� ���� �κ�� �Ѿ�ϴ�";
        titleTexts[2] = "���̺갡 ������ �� �÷��̾��� �г��Ӱ� �ְ� ����� Ȯ���� �� �ֽ��ϴ�";
        titleTexts[3] = "���� �������� ���� �÷��̾��� ���̺긦 ������ �� �ֽ��ϴ�";

        lobbyTexts[0] = "�κ� ȭ�鿡�� ���� ����, ���׷��̵�, ��Ƽ��Ʈ ������ ����� �̿��� �� ������ �ٽ� Ÿ��Ʋ�� ���ư� ���� �ֽ��ϴ�";

        upgradeTexts[0] = "���׷��̵忡�� ������ ����� �÷��̾��� �ɷ�ġ�� �ø� �� �ֽ��ϴ�";
        upgradeTexts[1] = "�÷��̾��� �ɷ�ġ�� �̰��� ǥ�õ˴ϴ�";
        upgradeTexts[2] = "������ ������ �̰��� ǥ�õ˴ϴ�";
        upgradeTexts[3] = "�� �ɷ�ġ�� �ش�Ǵ� �������� ���� ��ȭ�� �� �� �ֽ��ϴ�";
        upgradeTexts[4] = "��ȭ�� �ʿ��� ������ �̰��� ǥ�õ˴ϴ�";
        upgradeTexts[5] = "��ȭ ����Ʈ�� �� á�� ��� ��ȭ�� ���� �ʽ��ϴ�.";

        artifactTexts[0] = "��Ƽ��Ʈ ���������� ���ݱ��� ȹ���ߴ� ��Ƽ��Ʈ�� ������ �� �� �ֽ��ϴ�";
        artifactTexts[1] = "ȹ���� ��Ƽ��Ʈ�� ��� ���� ������ �˴ϴ�";
        artifactTexts[2] = "��Ƽ��Ʈ�� ���� ��ȣ�� �̸� ��ް� �Ӽ� �� �ɷ�ġ�� ���� ������ �� �� �ֽ��ϴ�";

        inGameTexts[0] = "���� ���� �� ����� Ÿ�̸ӿ� 10���� �ð��� �־����� �� �־��� �ð� ���� �ִ��� ���� ���͸� ������ �����ϴ� ���� ������ ��ǥ�Դϴ�";
        inGameTexts[1] = "�÷��̾�� ���̽�ƽ���� ������ �� ������ ���� â���� ���̽�ƽ�� ������ �ٲ� �� �ֽ��ϴ�";
        inGameTexts[2] = "�÷��̾�� ���Ͱ� ���� �� ü���� �����ϸ� �÷��̾� �Ӹ� ���� ü�� �ٸ� ���� Ȯ���� �����մϴ�";
        inGameTexts[3] = "������ ���� ������ ���Ͱ� ������ �Ǹ� �ڵ����� �����ϰ� �˴ϴ�";
        inGameTexts[4] = "���ʹ� �÷��̾ ���� �̵��ϸ� �ð��� �������� ������ ���Ͱ� �����ϰ� �˴ϴ�";
        inGameTexts[5] = "���͸� ���� �� �������� �����Ǹ� �� �������� �÷��̾�� ���� �� ȹ���� �� �ֽ��ϴ�";
        inGameTexts[6] = "����ġ ������ �������� �ٸ� ����ġ���� ������ �÷��̾��� ȹ�� ���� �� ����ġ ���� �ɷ�ġ�� ������ �޽��ϴ�";
        inGameTexts[7] = "�ڽ��� ��Ƽ��Ʈ�� �� �� �ְ� ���ݴϴ�";
        inGameTexts[8] = "ȸ�� ���� �÷��̾� �ִ� ü�� 20%�� ȸ������ �ݴϴ�";
        inGameTexts[9] = "�ڼ��� ��� ������ �÷��̾�� ����ɴϴ�";

        levelUpTexts[0] = "����ġ�� ���� á�� �� �������� �մϴ�";
        levelUpTexts[1] = "3���� �� �ϳ��� ��Ƽ��Ʈ�� ������ �� ������, �� ���������� �ѹ��� ���� ��ȸ�� �־����ϴ�";

        statInventoryTexts[0] = "�ڽ� ����� �������� ���� ����â�� �� �� �ֽ��ϴ�.";
        statInventoryTexts[1] = "����â������ �÷��̾��� �ɷ�ġ �� ���ݱ��� ȹ���� ��Ƽ��Ʈ�� ���� �� �� �ֽ��ϴ�";
        statInventoryTexts[2] = "��Ƽ��Ʈ�� ���� ��Ƽ��Ʈ�� ������ ������ �� �ֽ��ϴ�";

        attributeTexts[0] = "Ư���� ��Ƽ��Ʈ�� ȹ�� �� �Ӽ� ����Ʈ�� ����ϰ� �Ǹ�, �Ӽ��� ���� ������ �� �Ӽ��� ���� Ȯ�� �����մϴ�";
        attributeTexts[1] = "�� �Ӽ��� �ڼ��� ���� ���� Ȯ���� �����ϸ�, �Ӽ� ����Ʈ�� ���� �Ӽ��� ��ȭ�˴ϴ�";

        gameResultTexts[0] = "�־��� �ð��� ����ǰų� �÷��̾��� ü���� 0�� �Ǿ��� �� ���� ��� â�� ������ �Ǹ� ���� ������ ���� Ȯ���� �����մϴ�. �κ� ��ư�� ���� �κ�� ���ư� �� �ֽ��ϴ�.";

        settingTexts[0] = "���� â������ ���� �� ���̽�ƽ�� ���� ������ �����ϸ�, ���� ���� �� ���� ���Ⱑ �����մϴ�";
    }
}
