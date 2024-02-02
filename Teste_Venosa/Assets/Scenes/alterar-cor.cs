using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*Crio a classe ColorChanger que herda a classe MonoBehaviour*/
public class ColorChanger : MonoBehaviour
    {
    /*crio a váriavel apiURL com a URL da api*/
    private const string apiUrl = "http://localhost:3001/random-color";
    /*Aqui crio a váriavel Renderer da classe Renderer*/
    private Renderer Renderer;
    
    /*Crio a função Start onde ela começa assim que o script for carregado*/
    private void Start()
        {
        /* aqui associamos o Renderer ao objeto do unity que está associado o script*/
        Renderer = GetComponent<Renderer>();
        /* chama o método StartCoroutline que faz com que o método ChangeColorRountine seja executado de maneira assícrona para não impedir o funcinamento do resto do sistema*/
        StartCoroutine(ChangeColorRoutine());
        }

    /*Crio a função ChangeColorRoutine respónsavel pela troca de cor a cada 1 segundo*/
    private IEnumerator ChangeColorRoutine()
        {
        /* um repetidor infinito que atualizará a cor do objeto a cada 1 segundo*/
        while (true)
            {
            /*faz com que o script aguarde por 1 segundo*/
            yield return new WaitForSeconds(1f);
            /*Captura a cor do servidor*/
            StartCoroutine(GetColorFromServer());
            }
        }

    /*Pega a cor do servidor*/
    private IEnumerator GetColorFromServer()
        {
        /*Cria o objeto request da classe UnityWebRequest e solicita uma conexão com o servidor*/
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
            {
            /*Aguarda a solicitação com o servidor*/
            yield return request.SendWebRequest();

            /*if resultado for sucesso*/
            if (request.result == UnityWebRequest.Result.Success)
                {
                /*Cria a string colorJson e pega o valor json do servidor*/
                string colorJson = request.downloadHandler.text;
                /*Converte o json na classe ColorResponse*/
                ColorResponse colorResponse = JsonUtility.FromJson<ColorResponse>(colorJson);
                /*Passa a cor obtida para o objeto*/
                ChangeColor(colorResponse.color);
                }
            else
                {
                /*Debug padrão para saber quando der erro no processo*/
                Debug.LogError("Failed to get color from server");
                }
            }
        }

    /* Altera a cor do objeto associado ao script */
    private void ChangeColor(string hexColor)
        {
        /*Cria a váriavel color para armazenar a cor recebida*/
        Color color = new Color();
        /*Tenta converter a váriavel hexColor para color, se possivel entra dentro do scopo do if*/
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
            {
            /*Atualiza a cor do máterial associado ao script*/
            Renderer.material.color = color;
            }
        else
            {
            /*Deboug padrão para erro*/
            Debug.LogError("Failed to parse color");
            }
        }

    [System.Serializable]
    /*classe para converter Json em Unity*/
    private class ColorResponse
        {
        /*Cria a string*/
        public string color;
        }
    }

