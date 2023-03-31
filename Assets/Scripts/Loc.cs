using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loc : MonoBehaviour
{
    public enum locale {EN, RU};
    static public locale currentLocale = locale.RU;

    static public Dictionary<string, string> ENlocale;
    static public Dictionary<string, string> RUlocale;    

    private void Awake()
    {
        Init();
    }

    static public string Get(string key)
    {
        if (ENlocale == null)
            Init();

        if (currentLocale == locale.EN)
        {
            if (ENlocale.ContainsKey(key))
                return ENlocale[key];
            //else
                //Debug.Log("Error, no translation, EN " + key);
        }
        else if (currentLocale == locale.RU)
        {
            if (RUlocale.ContainsKey(key))
                return RUlocale[key];
            //else
                //Debug.Log("Error, no translation, RU " + key);
        }
        return key;
    }

    static public void Init() {
        ENlocale = new Dictionary<string, string>();
        RUlocale = new Dictionary<string, string>();
        
        ENlocale.Add("feedDesc", "Hunt phase. Choose a method.\n");
        RUlocale.Add("feedDesc", "Фаза охоты. Надо выбрать способ.\n");

        RUlocale.Add("feedNameAssault", "Нападение на прохожего в темной подворотне");
        RUlocale.Add("feedNameSeduce", "Соблазнение случайного встречного в баре");
        RUlocale.Add("feedNameAnimals", "Охота на животных");
        RUlocale.Add("feedNameSleep", "Скрытное пронитковение и кража у спящих");

        RUlocale.Add("huntSuccess", "Охота прошла успешно.\n");

        RUlocale.Add("huntAssaultFail1", "Вам удалось поймать несчастного прохожего, все шло хорошо, но закончив свое грязное дело вы поднимаете глаза и видите вдали видео камеру.\n" +
                        "Ее направление и красный огонек не оставляют сомнений, вы попали на запись.\n");
        RUlocale.Add("huntAssaultFail2", "Атака прошла плохо, жертва оказалась слишком хорошо подготовленна, или просто удача была на ее стороне.\n " +
                        "Гдето на улицах города есть человек видевший ваше истинное лицо.\n");

        RUlocale.Add("huntSeduceFail1", "Ночь откровенно не задалась. У одной девушки из под земли выпрыгнул ее парень. Другая прочитала вам лекцию о неподобающем поведении. \n" +
                        "Быть может вы просто вампир не из тех самых книжек и фильмов. \n" +
                        "Уже отчаявшись, вы все же находите спутницу желающую провеси с вами ночь. \n " +
                        "Приглашаете ее к себе, но нетерпение подводит, и вы начинаете прямо в соседней подворотне.\n " +
                        "Однако через минуту вас грубо прерывает ее... сутенер?! \n" +
                        "Мгновение паники, нужно чтото быстро сделать со свидетем. Кровь застелает глаза и еще минуту спустя вы стоите над свежим трупом. \n" +
                        "Вы чувствуете, что скоро на шум прибудет больше свидетелей, наспех прикрываете тело мусором и покидаете место проишествия.\n");
        RUlocale.Add("huntSeduceFail2", "Охота провалилась, остался след. Да, без описания. Я кстати ищу нарративщика\n"); // заменить

        RUlocale.Add("huntAnimalsFail1", "Охота провалилась, остался след. Да, без описания. Я кстати ищу нарративщика\n"); // заменить
        RUlocale.Add("huntAnimalsFail2", "Охота провалилась, остался след. Да, без описания. Я кстати ищу нарративщика\n"); // заменить
        RUlocale.Add("huntSleepFail1", "Охота провалилась, остался след. Да, без описания. Я кстати ищу нарративщика\n"); // заменить
        RUlocale.Add("huntSleepFail2", "Охота провалилась, остался след. Да, без описания. Я кстати ищу нарративщика\n"); // заменить

        RUlocale.Add("next", "Дальше");

        RUlocale.Add("researchDesc", "Есть время чтобы посмотреть и поспорашивать. Расследования могут принести новые улики.\n");
        RUlocale.Add("researchRandom", "Случайный поиск зацепок без цели.");
        RUlocale.Add("researchEvidence", "Пропаботка имеющейся улики.");
        RUlocale.Add("researchChar", "Копать под конкретного персонажа");
        RUlocale.Add("researchSelf", "Зачистить следы за собой");

        RUlocale.Add("introduction", "Вы вампир. Вот. Шпионьте, шантажируйте, боритесь за власть и все такое прочее. Да, я ищу наративщика. \n");//переписать

        RUlocale.Add("evidenceFound", "Была обнаружена улика\n");//переписать
        RUlocale.Add("noEvidence", "Ничего не найдено, возможно нечего искать\n");
        RUlocale.Add("evidenceReserchSucess", "Расследование улучшило улику\n");
        RUlocale.Add("evidenceReserchFail", "Расследование провалилось\n");

        RUlocale.Add("negotiationsImproveRelations", "Улучшить отношение");
        RUlocale.Add("negotiationsScare", "Напугать");
        RUlocale.Add("negotiationsBlackmail", "Начать шантаж");
        RUlocale.Add("negotiationsPublishEvidence", "Опубликовать улику");
        RUlocale.Add("negotiationsPressure", "Напомнить о долге");
        RUlocale.Add("negotiationsRelifPressure", "Снять давление с себя");

    }
}
