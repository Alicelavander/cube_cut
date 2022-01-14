using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cubes;

namespace Cube_cut
{
    public sealed class GameManager
    {
        private static GameManager instance = new GameManager();
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        private List<QuestionData> questionDatas = new List<QuestionData>();

        private GameManager()
        {
        }

        public List<int> WrongAnswers()
        {
            List<int> number = new List<int>();
            for(int i=0; i<questionDatas.Count; i++)
            {
                if (!questionDatas[i].IsCorrect) number.Add(i+1);
            }

            return number;
        }

        public void Reset()
        {
            questionDatas.Clear();
        }

        public void Add(QuestionData questiondata)
        {
            questionDatas.Add(questiondata);
        }

        public int QuestionNumber()
        {
            int questionNumber = questionDatas.Count;
            return questionNumber;
        }

        public QuestionData CurrentQuestion
        {
            get
            {
                return questionDatas[questionDatas.Count - 1];
            }
        }
 
    }

    public class QuestionData
    {
        public int answer = Random.Range(0, 4);
        public int selectedAnswer = -1;
        public List<List<Vector3>> answerOptions = new List<List<Vector3>>();

        public QuestionData(List<List<Vector3>> answerOptions)
        {
            this.answerOptions = answerOptions;
        }

        public bool IsCorrect
        {
            get
            {
                return answer == selectedAnswer;
            }
        }
    }
}

