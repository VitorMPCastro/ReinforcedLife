# ReinforcedLife

Um jogo estilo *Spore* onde uma Inteligência Artificial aprende a sobreviver na pele das células que você cria. Você também pode agir como um "deus", alterando o ecossistema em que a IA habita para observar seu comportamento e aprendizado.

Este projeto utiliza o plugin **ML-Agents** da Unity para treinar o "cérebro" do agente com algoritmos de Aprendizado por Reforço (PPO).

## Pré-requisitos

Antes de começar, garanta que você tenha o seguinte software instalado:

  * **Unity Hub**
  * **Unity Editor 2023.2.20f1** (ou uma versão compatível)
  * **Python 3.10.8** (é altamente recomendado usar esta versão específica para garantir consistência entre a equipe)
  * **Git**

## ⚙️ Configuração do Ambiente

Siga estes passos para configurar o projeto na sua máquina local.

### 1\. Ambiente Unity

1.  Clone este repositório para o seu computador:
    ```bash
    git clone https://github.com/VitorMPCastro/ReinforcedLife.git
    ```
2.  Abra o **Unity Hub** e adicione o projeto clonado à sua lista de projetos.
3.  Abra o projeto no Unity. O editor irá importar todos os assets.
4.  Verifique se o pacote **ML-Agents** (versão 3.0.0) está corretamente instalado. Você pode fazer isso em `Window > Package Manager` (no menu superior).

### 2\. Ambiente Python

O treinamento dos agentes é feito através de um ambiente Python separado. As instruções abaixo garantem que todos na equipe usem as mesmas dependências.

1.  Abra um terminal (PowerShell, Command Prompt, Git Bash, etc.).
2.  Navegue até a pasta raiz do projeto que você clonou (`ReinforcedLife`).
    ```bash
    cd caminho/para/ReinforcedLife
    ```
3.  Crie um ambiente virtual Python usando a versão 3.10. Este ambiente será uma pasta local que conterá todas as bibliotecas Python necessárias, mantendo seu sistema limpo.
    ```bash
    py -3.10 -m venv mlagents-env
    ```
4.  Ative o ambiente virtual. **(Este passo deve ser repetido toda vez que você abrir um novo terminal para trabalhar no projeto).**
      * No Windows:
        ```bash
        .\mlagents-env\Scripts\activate
        ```
      * No Mac/Linux:
        ```bash
        source mlagents-env/bin/activate
        ```
5.  Com o ambiente ativo, instale todas as dependências necessárias com um único comando. Ele lerá o arquivo `requirements.txt` e instalará as versões exatas de cada pacote.
    ```bash
    pip install -r requirements.txt
    ```

Seu ambiente de desenvolvimento está pronto\!

## 🚀 Como Treinar o Agente

Para iniciar uma sessão de treinamento, siga esta ordem de operações:

1.  Certifique-se de que seu ambiente Python está **ativado** no terminal (o prompt deve começar com `(mlagents-env)`).

2.  No terminal, na pasta raiz do projeto, execute o comando de aprendizado. Você pode dar um nome único para cada "rodada" de treino usando a flag `--run-id`.

    ```bash
    mlagents-learn player_config.yaml --run-id=PrimeiroTreino
    ```

3.  Aguarde até que a logo do ML-Agents apareça e a seguinte mensagem seja exibida no terminal:
    **`[INFO] Listening on port 5004. Start training by pressing the Play button in the Unity Editor.`**

4.  Com o treinador esperando, volte para o Editor da Unity e aperte o botão **Play**.

O treinamento começará. Você verá as estatísticas sendo impressas no terminal a cada passo.

### Visualizando o Progresso (Opcional)

Para ver gráficos do aprendizado em tempo real:

1.  Abra um **segundo terminal**.
2.  Ative o mesmo ambiente Python e navegue para a mesma pasta do projeto.
3.  Execute o seguinte comando:
    ```bash
    tensorboard --logdir results
    ```
4.  Abra o endereço `http://localhost:6006` no seu navegador para ver os gráficos de recompensa e outras métricas.

## 🛠️ Tecnologias Utilizadas

  * **Motor de Jogo:** Unity `2023.2.20f1`
  * **IA:** Unity ML-Agents `3.0.0`
  * **Linguagem de Scripting:** C\#
  * **Ambiente de Treinamento:** Python `3.10.8`
  * **Framework de Machine Learning:** PyTorch (instalado como dependência do ml-agents)

## 👨‍💻 Autores

  * Vitor Lucas Castro
  * Gabriel Rios dos Santos
  * Alexandre Yamaguishi
  * Bruno da Silva Favaro