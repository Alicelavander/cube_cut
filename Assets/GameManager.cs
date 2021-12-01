using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeSetup;

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
        public List<Senbun> CubeSidesList;

        private GameManager()
        {
        }


        public int WrongAnswerNumber()
        {
            int number = 0;
            for(int i=0; i<questionDatas.Count; i++)
            {
                if (!questionDatas[i].IsCorrect) number++;
            }

            return number;
        }

        public void Add(QuestionData questiondata)
        {
            questionDatas.Add(questiondata);
        }

        public int questionNumber()
        {
            int questionNumber = questionDatas.Count + 1;
            return questionNumber;
        }

        public QuestionData currentQuestion
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

