package com.uit.telescopio.topicos.controletelescopio;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ProgressBar;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class CameraActivity extends AppCompatActivity {

    Button takePicture;
    ImageButton camera, manager, pictures;
    ProgressBar progressBar;
    ImageView photo;

    private Handler handler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getSupportActionBar().hide();
        setContentView(R.layout.activity_camera);


        camera = (ImageButton) findViewById(R.id.camera);
        manager = (ImageButton) findViewById(R.id.manager);
        pictures = (ImageButton) findViewById(R.id.pictures);
        takePicture = (Button) findViewById(R.id.takePicture);
        progressBar = (ProgressBar) findViewById(R.id.progressBar);
        photo = (ImageView) findViewById(R.id.photo);

        progressBar.setVisibility(View.INVISIBLE);
        camera.setClickable(false);

        manager.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(CameraActivity.this, MainActivity.class));
            }
        });

        pictures.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(CameraActivity.this, PicturesActivity.class));
            }
        });

        takePicture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //desabilitar os outros botoes e exibe a progressBar enquanto espera uma resposta do servidor
                camera.setClickable(false);
                manager.setClickable(false);
                pictures.setClickable(false);
                takePicture.setClickable(false);
                progressBar.setVisibility(View.VISIBLE);

                new Thread(){
                   public void run() {

                       Bitmap image = null;

                       try {
                           URL url = new URL("http://img.ibxk.com.br/2016/02/29/29154120244089.jpg?w=1040");
                           HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                           InputStream input = connection.getInputStream();
                           image = BitmapFactory.decodeStream(input);
                       }catch (Exception e) {
                           e.printStackTrace();
                       }

                       final Bitmap imageAux = image;
                       handler.post(new Runnable() {
                           @Override
                           public void run() {
                               photo.setImageBitmap(imageAux);
                           }
                       });
                   }

                }.start();

                //habilitar os botoes e oculta a progressBar novamente quando o servidor responder
                camera.setClickable(true);
                manager.setClickable(true);
                pictures.setClickable(true);
                takePicture.setClickable(true);
                progressBar.setVisibility(View.INVISIBLE);
            }
        });
    }
}
