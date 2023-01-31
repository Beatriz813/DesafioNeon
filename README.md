# DesafioNeon

# Processador de depósito

### O que o serviço faz?
O processador recupera um lote de transações de depósito para validar e efetivar cada um deles.

As contas cadastradas são recuperadas da seguinte API: https://run.mocky.io/v3/7f0acd4b-e63d-4571-b834-c3db15f70673
O lote de transações é recuperado da seguinte API: https://run.mocky.io/v3/68cc9f8b-519b-4057-bf3c-804115e68fd4

Neste serviço é validado as seguintes informações:
  - Se a conta de origem não existe no cadastro do banco
  - Se a conta de destino não existe no cadastro do banco
  - Se o nome que vem informado na transação é diferente ao registrado no cadastro para a conta de destino
  - Se a agência e conta de destino estão vazias ou possuem caracteres que não são dígitos
  - Se a agência e conta de origem estão vazias ou possuem caracteres que não são dígitos
  - Se o valor a ser depositado é menor ou igual a zero
 
 Caso uma das afirmações acima for verdadeira a transação será processada com erro e seu log será registrado junto com a mensagem e status de erro.
 
 Caso as afirmações acima não forem verdadeiras o fluxo da transação continuará e transação será efetivada debitando a conta origem e creditando a conta destino.
 
 ### Sobre o Log
 
 O log das transações está sendo armazenado em uma instância de MongoDB no banco Neon dentro da collection LogTransacao.
 
 # API Consulta transação de depósito
 
 ### O que a API faz?
 
 A API consulta o banco de LogTransação para devolver ao cliente o status da transação.
 
 Esta API oferece 3 endpoints:
  - Consultar todas as transações (/transacoes)
  - Consulta as transações filtrando por seu status (/transacoes/status/{status})
  - Consulta as transações filtrando por seu id de transacao (/transacoes/idTransacao/{idTransacao})
