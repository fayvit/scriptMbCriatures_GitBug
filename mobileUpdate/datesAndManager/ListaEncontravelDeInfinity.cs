using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ListaEncontravelDeInfinity
{

    public static List<encontravel> EncontraveisDaqui
    {
        get {
            List<encontravel> retorno = new List<encontravel>();
            NomesCenas nomeDaCena = (NomesCenas)System.Enum.Parse(typeof(NomesCenas), SceneManager.GetActiveScene().name);
            Vector3 pos = GameController.g.Manager.transform.position;

            switch (nomeDaCena)
            {
                case NomesCenas.MbFlorestaLesteDeInfinity:
                case NomesCenas.MbFlorestaOesteDeInfinity:
                case NomesCenas.MbInfinity:
                    retorno = new List<encontravel>()
                        {
                            new encontravel(nomesCriatures.Arpia,2,4,7),
                            new encontravel(nomesCriatures.Babaucu,1.5f,4,7),
                            new encontravel(nomesCriatures.Marak,1,4,7),
                            new encontravel(nomesCriatures.Escorpion,1,4,7),
                            new encontravel(nomesCriatures.Iruin,1,4,7)
                        };
                    if (pos.x > 410 && pos.z > 1030 && pos.x < 700 && pos.z < 1090)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Nessei, 0.25f, 4, 7));
                    }

                    if (pos.x > 10 && pos.z > 720 && pos.x < 200 && pos.z < 1030)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Xuash, 0.25f, 4, 7));
                    }

                    if (pos.x > 770 && pos.z > 750 && pos.x < 990 && pos.z < 1090)
                    {
                        retorno.Add(new encontravel(nomesCriatures.PolyCharm, 0.25f, 4, 7));
                    }

                    if (pos.x > 850 && pos.z > 580 && pos.x < 890 && pos.z < 660)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Cracler, 0.5f, 4, 7));
                    }

                break;
                case NomesCenas.fortalezaStealer:
                    retorno = new List<encontravel>()
                        {
                            new encontravel(nomesCriatures.Arpia,1,4,7),
                            new encontravel(nomesCriatures.Babaucu,1.5f,5,8),
                            new encontravel(nomesCriatures.Marak,1,4,7),
                            new encontravel(nomesCriatures.Escorpion,1,4,7),
                            new encontravel(nomesCriatures.Iruin,1,4,7),
                            new encontravel(nomesCriatures.Aladegg,1.1f,4,7),
                            new encontravel(nomesCriatures.Onarac,0.5f,4,7),
                            new encontravel(nomesCriatures.Steal,0.2f,4,7),
                            new encontravel(nomesCriatures.Flam,0.35f,4,7),
                        };
                break;
                case NomesCenas.MbFlorestaOesteDeIve:
                case NomesCenas.FlorestaLesteDeIve:
                case NomesCenas.MbIve:
                    retorno = new List<encontravel>()
                        {
                            new encontravel(nomesCriatures.Arpia,1,4,7),
                            new encontravel(nomesCriatures.Babaucu,1.5f,5,8),
                            new encontravel(nomesCriatures.Marak,1,4,7),
                            new encontravel(nomesCriatures.Escorpion,1,4,7),
                            new encontravel(nomesCriatures.Iruin,1,4,7),
                            new encontravel(nomesCriatures.Aladegg,1.1f,4,7),
                            new encontravel(nomesCriatures.Onarac,0.5f,4,7),
                        };
                    if( (pos.x > 0 && pos.z > 310 && pos.x < 100 && pos.z < 420)
                        ||(pos.x > 860 && pos.z > 240 && pos.x < 990 && pos.z < 450))
                    {
                        retorno.Add(new encontravel(nomesCriatures.Serpente, 0.5f, 4, 7));
                    }


                    if (pos.x > 850 && pos.z > 510 && pos.x < 900 && pos.z < 570)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Cracler, 0.5f, 4, 7));
                    }

                    if (pos.x > 10 && pos.z > 80 && pos.x < 240 && pos.z < 220)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Florest, 0.25f, 4, 7));
                    }

                    if (pos.x > 410 && pos.z > 10 && pos.x < 570 && pos.z < 250)
                    {
                        retorno.Add(new encontravel(nomesCriatures.Cabecu, 0.5f, 4, 7));
                        retorno.Add(new encontravel(nomesCriatures.Rocketler, 0.5f, 4, 7));
                        retorno.Add(new encontravel(nomesCriatures.Baratarab, 0.5f, 4, 7));
                        retorno.Add(new encontravel(nomesCriatures.DogMour, 0.5f, 4, 7));
                        retorno.Add(new encontravel(nomesCriatures.Urkan, 0.5f, 4, 7));
                    }

                break;
                
            }
            return retorno;
        }
    }
}
