using UnityEngine;
using System.Collections.Generic;

public static class TextosIve
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
    {
        {ChaveDeTexto.ive1,new List<string>()
                    {
                        "Se você seguir para oeste daqui você chegará ao <color=yellow>rio Mariinque.</color>",
                        "Assim que você ultrapassar as montanhas logo verá a<color=yellow>Torre da Vida Eterna</color>",
                        "No alto da torre fica o castelo do imperador",
                        "Certeza que é lá que você quer ir?"
                    }},
                {ChaveDeTexto.ive2,new List<string>()
                    {
                        "Bem-vindo a cidade de <color=yellow>Ive</color>",
                        "Nossa cidade sempre foi próspera devido a rota comercial que liga a cidade de <color=yellow>Afarenga</color> dentro do vulcão do outro lado do rio Mariinque,",
                        "à cidade de <color=yellow>Ofawing</color> que fica além da ferrovida de <color=yellow>Jadme</color>.",
                        "Mas parece que os ventos não sopram a favor da nossa cidade",
                        "Alguns anos atrás <color=orange>Logan</color> mandou fechar a ferrovia de Jadme",
                        "Isso reduziu o fluxo de viajantes, pois não poderiam mais chegar a Ofawing por essa rota",
                        "E há pouco tempo um deslizamento fechou a entrada do vulcão que dava acesso a Afarenga",
                        "Nossa rota comercial está indo de mal a pior."
                    }},
                {ChaveDeTexto.ive3,new List<string>()
                    {
                        "No meio do <color=yellow>rio Mariinque</color> existe uma plataforma de extração de petróleo.",
                        "A <color=yellow>Petrolífera</color> é responsável pelo abastecimento de combustível de várias planícies de Orion",
                        "Mas ultimamente eles atravessam sérios problemas.",
                        "Alguns compartimentos da petrolífera foram tomados por Criatures <color=orange>Iziculos</color>.",
                        "Iziculos são pequenos criatures insetos cortadores que gostam de viver em lugares úmidos e escuros",
                        "carregam um bastão e com esse bastão podem aprender habilidades cortantes fortes o suficiente para cortar barras de aço",
                        "Eu acho que a Petrolífera está em sérios apuros"
                    }},
                {ChaveDeTexto.ive4,new List<string>()
                    {
                        "Os esgotos que correm por baixo dessa planície são o abrigo de Criatures asquerosos.",
                        "Viajantes que utilizaram a estrada leste da <color=yellow>Fortaleza</color> sempre tem histórias para contar sobre insetos que saiam das tubulações",
                        "Em vista desse problema, a administração pública da <color=yellow>Garra Governamental</color> colocou grades nas saidas de esgoto",
                        "Mas isso não resolveu completamente o problema",
                        "Alguns criatures que carregam bastões pode utilizar a habilidade <color=cyan>Sabre de Bastão</color>",
                        " com ela podem cortar as barras de isolamento do esgoto",
                        "A Garra Governamental está sempre repondo as grades cortadas...",
                        " mas de tempos em tempos aparece uma nova abertura provocada por Criatures."
                    }},
                {ChaveDeTexto.ive5,new List<string>()
                    {
                        "Atracado nas margens do <color=yellow>rio Mariinque</color> está o <color=yellow>Cruzador de Guerra </color>.",
                        "O cruzador de guerra é um navio construido para defender o continente de Yoro das invasões dos outros continentes.",
                        "Com o prolongar dos tempos de paz, o Cruzador se tornou pouco útil para questões de defesa territorial.",
                        "Então os militares resolveram abrir o Cruzador para visitação por 100 CRISTAIS.",
                        "Eles querem nos enganar dizendo que é para a população conhecer os aparelhos de segurança que nos protegem.",
                        " mas eu acredito que estão mesmo é querendo ganhar uns trocos",
                        "Possivelmente estão passando por uma crise financeira já que mantém um equipamento de guerra precisando de manutenção...",
                        " e praticamente sem utilidade."
                    }},
                {ChaveDeTexto.ive6,new List<string>()
                    {
                        "A tecnologia <color=cyan>Armagedom</color> foi criada a algumas décadas pelo próprio imperador <color=orange>Logan</color>",
                        " ele era estudante da universidade no deserto da predominância.",
                        "Com Armagedom's podemos transportar nossos itens de um Armagedom para outro entre os espalhados por Orion.",
                        "Isso é fascinaste!!",
                        "Tudo isso utilizando da energia única fornecida por um mineral bastante encontrado em Orion: as <color=cyan>gemas Laranges</color>.",
                        "Depois de Logan, muitos cientistas trabalharam em aprimoramentos para Armagedom.",
                        "A massa possível de ser transportada foi ampliada e o sistema de comunicação entre Armagedom's evoluiu muito desde Logan",
                        "mas a maior contribuição da tecnologia Armagedom, sem sombra de duvidas, foi feita por um jovem cientista",
                        " e atual milhonário: <color=orange>Jack Bandit</color>",
                        "a invenção dele foi, nada mais nada menos que, a <color=cyan>Luva de Guarde</color>.",
                        "Com a luva de Guarde podemos carregar nossos itens dentro de uma Gema Larange posicionada em nossas luvas.",
                        "E a coisa mais surpreendente das luvas de Guarde é a capacidade de carregar um certo número de Criatures vivos",
                        "Não é à toa que todo mundo em Orion é obrigado a utilizar Luvas de Guarde"
                    }},
                {ChaveDeTexto.ive7,new List<string>()
                    {
                        "Se você seguir por entre as montanhas do leste você chegará até a cidade de <color=yellow>Hadjek</color>.",
                        "Hadjek é passagem obrigatória para todos os viajantes que seguem o destino de <color=yellow>Jadme</color>.",
                        "No deserto de Jadme existe uma pirâmide bastante curiosa. Dizem os mais velhos que ela é na verdade um templo para <color=cyan>Turabá</color>,",
                        " o deus dos Mistérios.",
                        "O <color=orange>capitão Aramis</color> foi enviando numa missão dentro da pirâmide.",
                        "Eu desconfio que ele está procurando algum sacerdote de Turabá lá dentro.",
                        " Seria a busca da revelação sobre o mistério do comportamento atípico do imperador?"
                    }},
                {ChaveDeTexto.ive8,new List<string>()
                    {
                        "No fundo do rio Mariinque os clérigos de <color=cyan>Drag</color> construiram uma arena para o deus das águas.",
                        "O responsável pela <color=yellow>arena de Drag</color> é <color=orange>Omar Water</color>.",
                        "Dizem as lendas de Orion, que após o desvio do curso do rio Mariinque para a construção da represa,",
                        " Drag invocou seus seguidores para a construção de monumentos no fundo do rio.",
                        "Pela vontade de Drag, o novo rio, o rio desviado de seu curso, se tornou mais profundo e mais largo que o anterior.",
                        "Algumas gerações passaram desde o acontecido e então Omar chegou para ficar a frente da arena",
                        "Dizem que ele e sua esposa ouviram a própria voz de Drag lhes delegando a responsabilidade de reger a arena do Rio Mariinque ",
                        "Eu sou muito cético com relação a religiões,",
                        " acredito que essas histórias só reforçam o mito dos deuses para manter seus seguidores."
                    }},
                {ChaveDeTexto.ive9,new List<string>()
                    {
                        "O caminho para o castelo do imperador, a <color=yellow>Torre da Vida Eterna</color>, é fechada pelo acordo dos deus de Orion em proteger o imperador",
                        "No entando, os deuses não deram ao imperador uma proteção inabalável",
                        "Pela vontade dos deuses, um cidadão de Orion que possuir oito dos medalhões dos deuses pode abrir os portões da Torre",
                        "Para ganhar os medalhões dos deuses só existe uma maneira:",
                        "Procurar as arenas dos deuses e desafiar seus regentes para uma luta entre Criatures",
                        "Vencendo um regente de arena, na arena de um deus, você pode ganhar o medalhão desse Deus.",
                        "Os deuses deram essa possibilidade para que um cidadão digno pudesse destituir um imperador não digno",
                        "Mas eu acho que eles esqueceram da parte que o imperador lidera um exercito!!"
                    }},
                {ChaveDeTexto.ive10,new List<string>()
                    {
                        "A <color=yellow>Represa</color> que fica a norte da cidade de <color=yellow>Infinity</color> é responsável pelo abastecimento de água da maioria das planícies de Orion,",
                        " ela abastece todas as planícies de Yoro.",
                        "No rio Mariinque, existe uma grande <color=yellow>tubulação</color> fechada por um registro. Esses tubos se prolongam muito por Orion.",
                        "Talvez por dentro das tubulações você consiga chegar a planícies que não são acessíveis viajando por terra.",
                        "O controle dos registros das tubulações de Orion ficam na sede da <color=yellow>Garra Governamental</color>."
                    }},
                {ChaveDeTexto.ive11,new List<string>()
                {
                    "O caminho para a cidade de <color=yellow>Afarenga</color> foi obstruído por um deslizamento de terra há algum tempo.",
                    "O exercito do imperador <color=orange>Logan</color> em conjunto com as seções de serviços públicos da <color=yellow>Garra Governamental</color>...",
                    "... estão se mobilizando para desobstruir o caminho",
                    "De acordo com as noticias que correm em Orion, o general <color=orange>Potus Ramos</color> da Fortaleza Stealer...",
                    " já tem a disposição dele uma quantidade de explosivos suficiente para a realizar a tarefa",
                    "O que impede o serviço é um impasse entre as entidades imperialistas de Orion,",
                    " o exército liderado por Ramos espera mão de obra da Garra Governamental",
                    " e a Garra Governamental espera que a mão de obra seja aplicada pelo exercito",
                    "Como o material está com o General Ramos, talvez ele só precise delegar essa função a alguém de confiança",
                    "Você não quer se candidatar?"
                }},
                {ChaveDeTexto.ive12,new List<string>()
                {
                    " A história da construção da <color=yellow>fortaleza Stealer </color> é uma história muito antiga e que passa de geração para geração.",
                    " Por isso muitas versões tem informações que se desencontram.",
                    " Os mais velhos contam que o nome da fortaleza \" Stealer\" é em homenagem ao Criature de mesmo nome,",
                    " pois a floresta em volta da fortaleza era cheia desses Criatures",
                    " Porém, os Stealer\'s lançavam Relampagos contra os viajantes que atravessavam a floresta",
                    " Por esse motivo, desde a construção da fortaleza passando pelo tempo de atividade militar dela, Stealers foram caçados.",
                    "Hoje não existem mais Stealers na floresta da fortaleza.",
                    "Curioso que o Criature que deu nome a fortaleza foi exterminado justamente por causa da existencia da fortaleza",
                    "Algumas pessoas dizem ter visto Criatures Eletricos na floresta da Fortaleza,",
                    " mas a maioria das pessoas não acreditam nesses boatos",
                    "As pessoas que repetem isso normalmente ganham fama de mentirosas."
                }}
    };
}