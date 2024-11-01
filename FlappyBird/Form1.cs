using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8; //kolonlarin hareketi
        int gravity = 5;   //karakterin düşmesi, uçması
        int score = 0;    //puan
        public Form1()
        {
            InitializeComponent();
        }



        private void gameTimerEvent(object sender, EventArgs e) //oyunu başlatmak için kullanılan fonksiyon
        {
            flappyBird.Top += gravity; //karakterin düşmesini simüle eder
            pipeBottom.Left -= pipeSpeed; //kolonların sola doğru hareketi (ilerlemeyi simüle eder)
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score; 

            if(pipeBottom.Left < -150) //kolon ekrandan çıktığında ekranın ilerisinde yeni klon spawn olur
            {
                pipeBottom.Left = 780;
                score++; //kolona değmeden, kolon başarılı bir şekilde ekrandan çıkış yapıyorsa puan artar
            }

            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 750;
                score++;
            }

            if(flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds)) //karakter kolonlara veya yere değerse oyun biter
            {
                endGame();
            }

            if (score >= 6) //skor artışıyla oyunun zorluk seviyesi artar
            {
                pipeSpeed = 15;
            }

            if (flappyBird.Top < -25) //karkter ekranın üst sınırını aşarsa oyun biter
            {
                endGame();
            }
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e) //space bar2a basıldığında karakter yükselir
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -5;
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e) //space bar bırakıldığında karakter düşer
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 5;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over";
        }

    }
}
