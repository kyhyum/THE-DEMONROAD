using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDataParser : MonoBehaviour
{
    // 에디터에서 CSV 파일을 할당한다.
    public TextAsset skillDataCSV;

    public Dictionary<string, Dictionary<int, float>> ParseSkillData()
    {
        Dictionary<string, Dictionary<int, float>> skillDataDict = new Dictionary<string, Dictionary<int, float>>();

        // Split(param): param 단위로 나눈다.
        string[] lines = skillDataCSV.text.Split('\n');

        // 헤더 줄 파싱한다.
        // 헤더가 아마 레벨 부분일 듯
        string[] levelHeaders = lines[0].Split(',');

        for (int i = 1; i < levelHeaders.Length; i++)
        {
            // Trim(): 현재 문자열의 앞뒤쪽 공백을 제거한 문자열을 반환한다.
            levelHeaders[i] = levelHeaders[i].Trim();
        }

        // 데이터 줄 파싱한다.
        // Skip(param): 처음 위치에서 param개의 요소를 건너뛴 다음 나머지 데이터를 가져온다.
        // 첫 줄은 헤더라서 스킵한다.
        foreach (string line in lines.Skip(1))
        {
            string[] values = line.Split(',');

            string skillName = values[0].Trim();
            Dictionary<int, float> levelDamageDict = new Dictionary<int, float>();

            for (int i = 1; i < values.Length; i++)
            {
                // float.Parse(): string -> float 형변환한다.
                float damage = float.Parse(values[i]);
                levelDamageDict.Add(i, damage);
            }

            skillDataDict.Add(skillName, levelDamageDict);
        }

        return skillDataDict;
    }
}
