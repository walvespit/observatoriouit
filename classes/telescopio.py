from azure.storage.blob import BlockBlobService
from azure.storage.blob import PublicAccess #tornar container publico 
from azure.storage.blob import ContentSettings # carregar img

chaveDeAcesso = 'QO1mMxkATegD0+Kdy+oDffbiQzXcZTAzuLfl0FXONZddcEzqmiD1jKPKiuBk5hOMM8xapTN7cAWXdaPfxZR9FA=='
# acesso a conta
block_blob_service = BlockBlobService(account_name='observatorioitauna', account_key=chaveDeAcesso)

#criando um conainer para guardar os blobs (img)
block_blob_service.create_container('containerimg')
#tornar container publico
block_blob_service.set_container_acl('containerimg', public_access=PublicAccess.Container)

#carregando uma img no diretorio especifico
def carregaImagem(caminho_img, nome_imagem):
	if(block_blob_service.create_blob_from_path('containerimg',nome_imagem,caminho_img,
	content_settings=ContentSettings(content_type='image/jpg'))):
		print('imagem carregada com sucesso!')
	else:
		print('Erro ao carregar imagem imagem')



def excluirImagem(nome_imagem):
	#excluir blob
	block_blob_service.delete_blob('containerimg',nome_imagem)


# listar blobs
def listarImagem():
	generator = block_blob_service.list_blobs('containerimg')
	for blob in generator:
    		print(blob.name)

def downloadImagem(nome_imagem):
	#baixar do blob
	if block_blob_service.get_blob_to_path('containerimg','blob_box',nome_imagem):
		block_blob_service.get_blob_to_path('containerimg','blob_box',nome_imagem)
		print('sucesso')
	else:
		print("erro ao fazer download da imagem")


# ----------- TESTE ---------------------------#

print('\n\n listar imagens no repositorio:')
listarImagem()

print(' Inserir uma imagem:')
carregaImagem('/home/raphael/Imagens/img111.jpeg', 'objetivo')