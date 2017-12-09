package com.uit.telescopio.topicos.controletelescopio;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;

public class MainActivity extends AppCompatActivity {

    ImageButton camera, manager, pictures;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getSupportActionBar().hide();
        setContentView(R.layout.activity_main);


        camera = (ImageButton) findViewById(R.id.camera);
        manager = (ImageButton) findViewById(R.id.manager);
        pictures = (ImageButton) findViewById(R.id.pictures);

        manager.setClickable(false);

        camera.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(MainActivity.this, CameraActivity.class));
            }
        });

        pictures.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(MainActivity.this, PicturesActivity.class));
            }
        });
    }
}
